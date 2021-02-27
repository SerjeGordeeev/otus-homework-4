using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class EnemyTargetingInitializeSystem : IInitializeSystem, ITearDownSystem
{
    Contexts _contexts;
    private IGroup<GameEntity> _targetCharacterGroup;

    public EnemyTargetingInitializeSystem(Contexts contexts)
    {
        _contexts = contexts;
        _targetCharacterGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TargetEnemy));
    }

    public void Initialize()
    {
        _targetCharacterGroup.OnEntityAdded += OnTargetEnemyAdded;
        _targetCharacterGroup.OnEntityRemoved += OnTargetEnemyRemoved;
    }

    public void TearDown()
    {
        _targetCharacterGroup.OnEntityAdded -= OnTargetEnemyAdded;
        _targetCharacterGroup.OnEntityRemoved -= OnTargetEnemyRemoved;
    }

    public void OnTargetEnemyAdded(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component) {
        if (entity.isUserControlled)
        {
            var targetEnemy = ((TargetEnemyComponent)component).value;
            if (targetEnemy.targetIndicator.value)
            {
                var targetIndicator = targetEnemy.targetIndicator.value;
                targetIndicator.gameObject.SetActive(true);
                var sr = targetIndicator.gameObject.GetComponent<SpriteRenderer>();
                sr.color = Color.red;
            }
        }
    }

    public void OnTargetEnemyRemoved(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        var targetEnemy = ((TargetEnemyComponent)component).value;
        if (targetEnemy.targetIndicator.value)
        {
            var targetIndicator = targetEnemy.targetIndicator.value;
            targetIndicator.gameObject.SetActive(false);
        }
    }
}
