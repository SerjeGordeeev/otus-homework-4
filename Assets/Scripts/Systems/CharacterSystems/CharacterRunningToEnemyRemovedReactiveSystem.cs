using Entitas;
using System.Collections.Generic;

public class CharacterRunningToEnemyRemovedReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharacterRunningToEnemyRemovedReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            switch (entity.weapon.value)
            {
                case Weapon.Bat:
                    entity.ReplaceCharacterState(CharacterState.BeginAttack);
                    break;

                case Weapon.Fist:
                    entity.ReplaceCharacterState(CharacterState.BeginPunch);
                    break;
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCharacter && entity.hasWeapon;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(
            GameMatcher.AllOf(GameMatcher.Character, GameMatcher.Weapon)
            .AnyOf(GameMatcher.RunningToEnemy)
            .Removed()
        );
    }
}