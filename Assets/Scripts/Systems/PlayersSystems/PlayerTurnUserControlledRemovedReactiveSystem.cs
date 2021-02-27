using Entitas;
using System.Collections.Generic;

public class PlayerTurnUserControlledRemovedReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public PlayerTurnUserControlledRemovedReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.targetEnemy.value.targetIndicator.value.gameObject.SetActive(false);
            foreach (var character in entity.army.value)
            {
                character.isTarget = false;
            } 
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.isUserControlled;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.PlayerTurn).Removed());
    }
}