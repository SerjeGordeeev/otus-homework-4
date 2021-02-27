using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PrefabReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public PrefabReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var prefab = entity.prefab.value;
            var parentTransform = entity.prefab.parentTransform;

            var instance = GameObject.Instantiate(prefab, parentTransform);

            entity.AddView(instance);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPrefab;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Prefab));
    }
}