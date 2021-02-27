using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Runtime.InteropServices.ComTypes;

public class PlayersInitializeSystem : IInitializeSystem
{
    GameContext _game;
    public PlayersInitializeSystem(Contexts contexts)
    {
        _game = contexts.game;
    }

    public void Initialize()
    {
        var gameEntity = _game.gameEntity;
        var players = _game.globals.value.players;

        var playersEntities = new List<GameEntity>();
        gameEntity.AddPlayersList(playersEntities);
        gameEntity.AddPlayerTurnIndex(0);


        int x = -10;
        Quaternion rotation = Quaternion.AngleAxis(90, Vector3.up);
        // Create players entities
        foreach (var playerData in players)
        {
            int z = 0;

            var playerEntity = _game.CreateEntity();
            playerEntity.AddName(playerData.name, true);
            playerEntity.isPlayer = true;

            switch (playerData.controlType)
            {
                case ControlType.Computer:
                    playerEntity.isAIControlled = true;
                    break;
                case ControlType.User:
                    playerEntity.isUserControlled = true;
                    break;
            }


            var playerUserArmy = new List<GameEntity>();
            playerData.army.ForEach((characterData) => {
                var characterPrefab = _game.characters.value.characters.Find((character) => character.type == characterData.type);
                // Debug.Log(characterPrefab);
                if (characterPrefab == null)
                {
                    Debug.LogError("CharacterPrefab not found with type: " + characterData.type);
                    return;
                }

                var characterEntity = _game.CreateEntity();
                characterEntity.AddPrefab(characterPrefab.gameObject, null);
                characterEntity.isCharacter = true;
                characterEntity.AddCharacterPlayer(playerEntity);
                characterEntity.AddName("Character_" + playerEntity.name.value, false);
                characterEntity.AddHealth(characterData.health);
                characterEntity.AddWeapon(characterData.weapon, characterData.damage);
                characterEntity.AddRunSpeed(characterData.runSpeed);
                characterEntity.AddDistanceFromTarget(characterData.distanceFromTarget);

                characterEntity.AddPosition(new Vector3(x, 0, z));
                characterEntity.AddOriginalPosition(characterEntity.position.value);

                characterEntity.AddRotation(rotation);
                characterEntity.AddOriginRotation(characterEntity.rotation.value);

                playerUserArmy.Add(characterEntity);

                z += 5;
            });

            x += 10;
            rotation *= Quaternion.AngleAxis(180, Vector3.up);

            playerEntity.AddArmy(playerUserArmy);
            playerEntity.AddAliveCharacters(new List<GameEntity>(playerUserArmy));
            playersEntities.Add(playerEntity);
        }


        // Set players enemies to each other
        foreach (var playerEntity in playersEntities)
        {
            var playerEnemy = playersEntities.Find(entity => entity != playerEntity);
            playerEntity.AddPlayerEnemy(playerEnemy);
        }
    }



}
