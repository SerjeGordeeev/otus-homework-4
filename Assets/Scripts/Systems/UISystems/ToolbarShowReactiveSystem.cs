using Entitas;
using System.Collections.Generic;

public class ToolbarShowReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public ToolbarShowReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var toolbar = _contexts.game.toolbarEntity;
            toolbar.view.value.SetActive(entity.isUserControlled);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.isPlayerTurn;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.PlayerTurn));
    }
}