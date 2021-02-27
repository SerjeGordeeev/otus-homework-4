using Entitas;
using System;
using System.Collections.Generic;

public class CharacterAttackReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharacterAttackReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var weapon = entity.weapon.value;
            CharacterState state;

            switch(weapon)
            {
                case Weapon.Bat:
                case Weapon.Fist:
                    state = CharacterState.RunningToEnemy;
                    entity.isRunningToEnemy = true;
                    break;
                case Weapon.Pistol:
                    state = CharacterState.BeginShoot;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            entity.ReplaceCharacterState(state);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCharacter && entity.hasAttack && entity.hasWeapon;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Character, GameMatcher.Attack, GameMatcher.Weapon));
    }
}