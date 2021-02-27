using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnIndexReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public PlayerTurnIndexReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var gameEntity = entities.SingleEntity();

        foreach (var player in gameEntity.playersList.value)
        {
            player.isPlayerTurn = false;
        }

        var turningPlayerIx = gameEntity.playerTurnIndex.value;
        gameEntity.playersList.value[turningPlayerIx].isPlayerTurn = true;
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isGame && entity.hasPlayerTurnIndex;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Game, GameMatcher.PlayerTurnIndex));
    }
}