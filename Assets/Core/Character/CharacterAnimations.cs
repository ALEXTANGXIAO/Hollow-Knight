using UnityEngine;

namespace Core.Character
{
    public class CharacterAnimations : MonoBehaviour
    {
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int IsJumping = Animator.StringToHash("IsJumping");
        public static readonly int StartJump = Animator.StringToHash("StartJump");
        public static readonly int Die = Animator.StringToHash("Die");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Dash = Animator.StringToHash("Dash");
    }
}