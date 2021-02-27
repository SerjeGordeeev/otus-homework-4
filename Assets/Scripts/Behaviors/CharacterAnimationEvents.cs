using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    GameEntity entity;

    void Start()
    {
        entity = GetComponentInParent<EntityAccessor>().entity;
    }

    void ShootEnd()
    {
        entity.ReplaceCharacterState(CharacterState.Idle);
    }

    void AttackEnd()
    {
        // TODO
        entity.ReplaceCharacterState(CharacterState.RunningFromEnemy);
    }

    void PunchEnd()
    {
        entity.ReplaceCharacterState(CharacterState.RunningFromEnemy);
    }

    void DoDamage()
    {
        entity.attack.value.ReplaceDamage(entity.weapon.damage);
    }
}
