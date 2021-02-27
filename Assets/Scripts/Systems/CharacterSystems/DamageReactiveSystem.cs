using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class DamageReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public DamageReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var healthAfterDamage = Mathf.Clamp(entity.health.value - entity.damage.value, 0, entity.health.value);
            entity.ReplaceHealth(healthAfterDamage);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth && entity.hasDamage;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Damage));
    }
}