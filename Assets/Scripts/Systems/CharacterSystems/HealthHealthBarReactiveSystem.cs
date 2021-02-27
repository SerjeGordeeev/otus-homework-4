using Entitas;
using System.Collections.Generic;

public class HealthHealthBarReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public HealthHealthBarReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.healthBar.value.text.text = entity.health.value.ToString();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth && entity.hasHealthBar;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.HealthBar, GameMatcher.Health));
    }
}