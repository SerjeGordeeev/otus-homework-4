using UnityEngine;

public class CharacterAnimatorStates
{
    public static readonly int Death = Animator.StringToHash("Death");
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int Punch = Animator.StringToHash("Punch");
    public static readonly int Shoot = Animator.StringToHash("Shoot");
    public static readonly int MeleeAttack = Animator.StringToHash("MeleeAttack");
}
