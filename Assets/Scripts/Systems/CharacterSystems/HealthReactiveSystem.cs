using Entitas;
using System.Collections.Generic;

public class HealthReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public HealthReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var healthValue = entity.health.value;
            if (healthValue == 0)
            {
                entity.isDead = true;
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Health));
    }
}