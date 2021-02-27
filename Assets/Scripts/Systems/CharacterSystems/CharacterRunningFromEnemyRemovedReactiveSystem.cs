using Entitas;
using System.Collections.Generic;

public class CharacterRunningFromEnemyRemovedReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharacterRunningFromEnemyRemovedReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.ReplaceCharacterState(CharacterState.Idle);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCharacter && !entity.isRunningFromEnemy;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.RunningFromEnemy).Removed());
    }
}