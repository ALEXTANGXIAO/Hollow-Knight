using UnityEngine;

namespace Core.Combat.Projectiles
{
    public class AcceleratingProjectile : AbstractProjectile
    {
        public float speed = 5.0f;

        private Vector3 direction;
        private Vector3 velocity;

        private void Start()
        {
            Invoke(nameof(DestroyProjectile), 6.0f);
        }

        public override void SetForce(Vector2 force)
        {
            this.force = force;
            direction = force.normalized;
        }

        void Update()
        {
            velocity += direction * speed * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }
}