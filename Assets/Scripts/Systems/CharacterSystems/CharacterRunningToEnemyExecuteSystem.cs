
using Entitas;
using System.Linq;
using UnityEngine;

public class CharacterRunningToEnemyExecuteSystem : IExecuteSystem
{
    Contexts _contexts;
    private IGroup<GameEntity> _runningToEnemyGroup;

    public CharacterRunningToEnemyExecuteSystem(Contexts contexts)
    {
        _contexts = contexts;
        _runningToEnemyGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Character, GameMatcher.RunningToEnemy));
    }

    public void Execute()
    {
        foreach (var characterEntity in _runningToEnemyGroup.GetEntities())
        {
            if(RunTowards(
                characterEntity.attack.value.position.value,
                characterEntity.distanceFromTarget.value,
                characterEntity))
            {
                characterEntity.isRunningToEnemy = false;
            }
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget, GameEntity characterEntity)
    {
        var position = characterEntity.position.value;

        Vector3 distance = targetPosition - position;
        if (distance.magnitude < 0.00001f)
        {
            characterEntity.ReplacePosition(targetPosition);

            return true;
        }

        Vector3 direction = distance.normalized;

        characterEntity.ReplaceRotation(Quaternion.LookRotation(direction));

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - position);

        Vector3 step = direction * characterEntity.runSpeed.value * Time.deltaTime;
        if (step.magnitude < distance.magnitude)
        {
            characterEntity.ReplacePosition(position + step);

            return false;
        }

        characterEntity.ReplacePosition(targetPosition);

        return true;
    }
}