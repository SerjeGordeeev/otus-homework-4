using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CharacterUserTargetingInitializeSystem : IInitializeSystem, ITearDownSystem
{
    Contexts _contexts;
    private IGroup<GameEntity> _targetCharacterGroup;

    public CharacterUserTargetingInitializeSystem(Contexts contexts)
    {
        _contexts = contexts;
        _targetCharacterGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TargetCharacter));
    }

    public void Initialize()
    {
        _targetCharacterGroup.OnEntityAdded += OnTargetCharacterAdded;
        _targetCharacterGroup.OnEntityRemoved += OnTargetCharacterRemoved;
    }

    public void TearDown()
    {
        _targetCharacterGroup.OnEntityAdded -= OnTargetCharacterAdded;
        _targetCharacterGroup.OnEntityRemoved -= OnTargetCharacterRemoved;
    }

    public void OnTargetCharacterAdded(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        if (entity.isUserControlled)
        {
            var targetCharacter = entity.targetCharacter;
            targetCharacter.value.isTarget = true;
            // Debug.Log("OnTargetCharacterAdded " + targetCharacter.value);
        }
   
    }

    public void OnTargetCharacterRemoved(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        if (entity.isUserControlled) {
            var targetCharacter = (TargetCharacterComponent)component;
            targetCharacter.value.isTarget = false;
            // Debug.Log("OnTargetCharacterRemoved " + targetCharacter.value);
        }
    }
}
