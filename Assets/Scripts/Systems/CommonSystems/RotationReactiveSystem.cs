using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class RotationReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public RotationReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            // Debug.Log(entity);
            entity.view.value.transform.rotation = entity.rotation.value;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasRotation;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Rotation));
    }
}