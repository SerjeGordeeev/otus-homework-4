using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTurnAIControlledReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public PlayerTurnAIControlledReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var aliveCharacters = entity.aliveCharacters.value;
            var targetCharacter = aliveCharacters[Random.Range(0, aliveCharacters.Count - 1)];
            entity.ReplaceTargetCharacter(targetCharacter);


            var enemyArmy = entity.playerEnemy.value.army.value.FindAll(characterEntity => characterEntity.isDead == false);
            var targetEnemy = enemyArmy.Aggregate((currentEnemy, nextEnemy) => currentEnemy.health.value < nextEnemy.health.value? currentEnemy : nextEnemy);

            entity.ReplaceTargetEnemy(targetEnemy);

            // -->
            targetCharacter.ReplaceAttack(targetEnemy);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.isPlayerTurn && entity.isAIControlled 
            && entity.hasAliveCharacters && entity.aliveCharacters.value.Count != 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.PlayerTurn).Added());
    }
}