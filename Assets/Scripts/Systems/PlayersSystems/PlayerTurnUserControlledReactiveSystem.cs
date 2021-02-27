using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnUserControlledReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public PlayerTurnUserControlledReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var aliveCharacters = entity.aliveCharacters.value;
            var targetCharacter = aliveCharacters[0];
            entity.ReplaceTargetCharacter(targetCharacter);

            var enemyArmy = entity.playerEnemy.value.aliveCharacters.value;


            var targetEnemy = entity.hasTargetEnemy ? entity.targetEnemy.value : enemyArmy[0];
            entity.ReplaceTargetEnemy(targetEnemy);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.isPlayerTurn && entity.isUserControlled
            && entity.hasAliveCharacters && entity.aliveCharacters.value.Count != 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.PlayerTurn).Added());
    }
}