using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameInitializeSystem : IInitializeSystem
{
    GameContext _game;
    public GameInitializeSystem(Contexts contexts)
    {
        _game = contexts.game;
    }

    public void Initialize()
    {
        // Инициализация сущности игрового процесса
        var gameEntity = _game.CreateEntity();
        gameEntity.isGame = true;
        gameEntity.AddName("Game", true);
        gameEntity.AddGameState(GameState.PLAYER_TURN);

        _game.SetMainCamera(Camera.main);
    }
}
