using System.Collections.Generic;
using Core.Combat;
using Core.Util;
using UnityEngine;

namespace Core.Character
{
    public class AttackController : MonoBehaviour
    {
        public Transform attackSourceTransform;
        public Transform attackTargetTransform;
        public float cooldown = 0.4f;
        public float attackRadius = 0.8f;
        public float attackRecoil = 200.0f;
        public float pogoRecoilMultiplier = 3.0f;
        public ParticleSystem thrustParticles;

        public LayerMask attackLayerMask;

        private float attackCooldown;
        private List<GameObject> attackVictims;
        private ParticleSystem currentThrustParticles;

        private PlayerController _playerController;

        void Awake()
        {
            _playerController = GetComponent<PlayerController>();

            attackVictims = new List<GameObject>(10);
            SwitchWeapon();
        }

        // Update is called once per frame
        public bool TryAttack()
        {
            if (attackCooldown <= 0)
            {
                // Add controller support with Right analog stick
                Vector3 attackDirection = new Vector3(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"), 0);

                attackDirection = attackDirection.normalized;

                if (attackDirection.x == 0 && attackDirection.y == 0)
                    attackDirection.x = _playerController.facingDirection;
                
                attackTargetTransform.position = attackSourceTransform.position + attackDirection * attackRadius;
                float tx = attackDirection.x;
                float ty = attackDirection.y;

                // Thrust
                float thrustAngle = -Vector2.SignedAngle(attackDirection, Vector2.right); // Invert
                var thrustQuaternion = Quaternion.Euler(0, 0, thrustAngle);
                attackTargetTransform.rotation = thrustQuaternion;

                currentThrustParticles.Play();

                float raySpacing = 6; // How much space between the raycasts (in degrees)
                for (int i = -2; i < 2; i++)
                {
                    float deltaAngle = i * raySpacing * Mathf.Deg2Rad;

                    // Rotate direction vector by angle offset
                    float sin = Mathf.Sin(deltaAngle);
                    float cos = Mathf.Cos(deltaAngle);

                    Vector2 targetDir = new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));

                    var hit = Physics2D.Raycast(attackSourceTransform.position, targetDir, attackRadius + 1.5f,
                        attackLayerMask);
                    Debug.DrawRay(attackSourceTransform.position, targetDir, Color.red);

                    if (hit)
                    {
                        if (attackVictims.Contains(hit.collider.gameObject))
                            continue;

                        attackVictims.Add(hit.collider.gameObject);

                        // First, check if it is a hittable
                        var hittable = hit.collider.GetComponent<Hittable>();
                        if (hittable != null)
                        {
                            hittable.OnAttackHit(hit.point, targetDir * 4.0f, 5);

                            // If it is an enemy, perform extra actions (e.g. player recoil)
                            var recoilForce = -targetDir * attackRecoil; // Player Recoil -> treat carefully!
                            if (hit.point.y < transform.position.y)
                                recoilForce.y *= pogoRecoilMultiplier;

                            recoilForce.y = Mathf.Max(-20.0f, recoilForce.y);
                            _playerController.DoRecoil(recoilForce, true);

                            // Minor camera shake
                            CameraController.Instance.ShakeCamera(0.05f, 0.5f);
                        }
                        else
                        {
                            // Otherwise, play default particles and SFX
                        }

                        attackCooldown = cooldown;
                    }
                }

                attackVictims.Clear();

                return true;
            }

            return false;
        }

        public void SwitchWeapon()
        {
            currentThrustParticles = thrustParticles;
        }

        private void Update()
        {
            if (attackCooldown > 0)
                attackCooldown -= Time.deltaTime;
        }
    }
}