using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Combat;
using UnityEngine;

namespace Core.AI
{
    public class EnemyConditional : Conditional
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected Destructable destructable;
        protected PlayerController player;
        
        public override void OnAwake()
        {
            body = GetComponent<Rigidbody2D>();
            player = PlayerController.Instance;
            destructable = GetComponent<Destructable>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }
}