using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class NextPlayerTurnReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public NextPlayerTurnReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var nextPlayerIndex = entity.playerTurnIndex.value + 1;
            if (nextPlayerIndex == entity.playersList.value.Count)
            {
                nextPlayerIndex = 0;
            }

            entity.ReplacePlayerTurnIndex(nextPlayerIndex);
            entity.isNextPlayerTurn = false;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isGame && entity.isNextPlayerTurn;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.NextPlayerTurn));
    }
}