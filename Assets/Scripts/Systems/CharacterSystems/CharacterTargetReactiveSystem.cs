using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharacterTargetReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            // Debug.Log(entity + " " + entity.isTarget);
            entity.targetIndicator.value.gameObject.SetActive(entity.isTarget);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTargetIndicator;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Target, GameMatcher.TargetIndicator).AddedOrRemoved());
    }
}