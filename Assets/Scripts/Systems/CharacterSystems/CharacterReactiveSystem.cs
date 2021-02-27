using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharacterReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var view = entity.view.value;
            entity.AddAnimator(view.GetComponentInChildren<Animator>());
            view.AddComponent<EntityAccessor>().entity = entity;
            entity.animator.value.gameObject.AddComponent<CharacterAnimationEvents>();

            var targetIndicator = view.GetComponentInChildren<CharacterTargetIndicator>();
            targetIndicator.gameObject.SetActive(false);
            entity.AddTargetIndicator(targetIndicator);


            var healthBar = view.GetComponentInChildren<CharacterHealthBar>();
            entity.AddHealthBar(healthBar);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCharacter && entity.hasView;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Character, GameMatcher.View));
    }
}