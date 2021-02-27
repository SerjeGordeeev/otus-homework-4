using Entitas;
using System;
using System.Collections.Generic;

public class CharacterStateReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharacterStateReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var animator = entity.animator.value;

            switch (entity.characterState.value)
            {
                case CharacterState.Idle:
                    entity.ReplaceRotation(entity.originRotation.value);
                    animator.SetFloat(CharacterAnimatorStates.Speed, 0f);
                    // entity.isTurnEnded
                    // gameEntity.nextTurn
                    var gameEntity = _contexts.game.gameEntity;
                    gameEntity.isNextPlayerTurn = true;
                    break;
                case CharacterState.RunningToEnemy:
                    animator.SetFloat(CharacterAnimatorStates.Speed, entity.runSpeed.value);
                    break;
                case CharacterState.BeginAttack:
                    animator.SetTrigger(CharacterAnimatorStates.MeleeAttack);
                    entity.ReplaceCharacterState(CharacterState.Attack);
                    break;

                case CharacterState.Attack:
                    break;

                case CharacterState.BeginShoot:
                    animator.SetTrigger(CharacterAnimatorStates.Shoot);
                    entity.ReplaceCharacterState(CharacterState.Shoot);
                    break;

                case CharacterState.Shoot:
                    break;

                case CharacterState.BeginPunch:
                    animator.SetTrigger(CharacterAnimatorStates.Punch);
                    entity.ReplaceCharacterState(CharacterState.Punch);
                    break;

                case CharacterState.Punch:
                    break;

                case CharacterState.RunningFromEnemy:
                    animator.SetFloat(CharacterAnimatorStates.Speed, entity.runSpeed.value);
                    entity.isRunningFromEnemy = true;
                    break;

                case CharacterState.BeginDying:
                    animator.SetTrigger(CharacterAnimatorStates.Death);
                    entity.ReplaceCharacterState(CharacterState.Dead);
                    break;

                case CharacterState.Dead:
                    break;
/*                default:
                    throw new ArgumentOutOfRangeException();*/
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCharacter && entity.hasAnimator && entity.hasCharacterState;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Character, GameMatcher.Animator, GameMatcher.CharacterState));
    }
}