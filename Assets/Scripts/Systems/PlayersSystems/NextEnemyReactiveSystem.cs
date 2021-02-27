using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class NextEnemyReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public NextEnemyReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var targetCharacter = entity.targetCharacter.value;
            var enemyPlayer = entity.playerEnemy.value;
            var enemyArmy = enemyPlayer.army.value;
            var aliveEnemyCharacters = enemyArmy.FindAll(characterEntity => characterEntity.isDead == false);
            var currentEnemyCharcaterIx = aliveEnemyCharacters
                .FindIndex((character) => character == entity.targetEnemy.value);

           
            currentEnemyCharcaterIx++;
            currentEnemyCharcaterIx = currentEnemyCharcaterIx == aliveEnemyCharacters.Count
                ? 0
                : currentEnemyCharcaterIx;

            // targetCharacter.ReplaceAttack(aliveEnemyCharacters[currentEnemyCharcaterIx]);
            entity.ReplaceTargetEnemy(aliveEnemyCharacters[currentEnemyCharcaterIx]);
            entity.isNextEnemy = false;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.isNextEnemy;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.NextEnemy));
    }
}