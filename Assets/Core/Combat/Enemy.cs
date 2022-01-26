using System;
using System.Collections;
using Core.Character;
using Core.Util;
using UnityEngine;

namespace Core.Combat
{
    public class Enemy : MonoBehaviour
    {
        public Destructable destructable;
        public ParticleSystem customDeathParticles;
        public AudioClip customDeathSound;
        public bool overrideSpatialAudio;
        public bool destroyOnDeath;
        public int goldReward;
        public string hitAnimationTrigger;

        public bool IsDead { get; protected set; }
        public bool CanMove { get; set; }
        public bool IsMoving { get; set; }
        public AudioSource AudioSource { get; private set; }
        protected Animator animator;
        private float baseScaleX;

        // Player reference
        protected PlayerController player;
        
        public virtual bool CanShoot => false;

        public event Action OnSelfHurt;
        public event Action OnPlayerHurt;
        public event Action OnDestroyed;

        protected virtual void Awake()
        {
            destructable.OnHit += OnAttackHit;
            destructable.OnDestroyed += OnDeath;

            animator = GetComponentInChildren<Animator>();
            AudioSource = GetComponent<AudioSource>();

            if (!overrideSpatialAudio)
            {
                AudioSource.rolloffMode = AudioRolloffMode.Linear;
                AudioSource.minDistance = 5.0f;
                AudioSource.maxDistance = 15.0f;
                AudioSource.spatialBlend = 1.0f;
            }

            baseScaleX = transform.localScale.x;

            CanMove = true;

            player = PlayerController.Instance;
        }

        public virtual void OnAttackHit(Vector2 position, Vector2 force)
        {
            if (hitAnimationTrigger != null)
                animator.SetTrigger(hitAnimationTrigger);

            OnSelfHurt?.Invoke();
        }

        public virtual void SetDead()
        {
            IsDead = true;
        }

        protected virtual void OnDeath()
        {
            IsDead = true;

            if (animator != null)
                animator.SetBool("IsDead", true);

            if (customDeathSound != null)
                SoundManager.Instance.PlaySoundAtLocation(customDeathSound, transform.position);

            if (customDeathParticles != null)
                EffectManager.Instance.PlayOneShot(customDeathParticles, transform.position + Vector3.up * 0.5f);

            // Minor camera shake
            CameraController.Instance.ShakeCamera(0.05f, 0.5f);

            if (destroyOnDeath)
                Destroy(gameObject);
        }

        public virtual void Respawn(Vector2 position)
        {
            destructable.Revive();
            IsDead = false;
            transform.position = position;

            if (animator != null)
                animator.SetBool("IsDead", false);
        }

        protected virtual void OnHitPlayer(Vector2 hitVelocity)
        {
            OnPlayerHurt?.Invoke();
        }

        public virtual void SetFacing(float direction)
        {
            if (direction > 0)
            {
                transform.localScale = new Vector3(baseScaleX, transform.localScale.y, transform.localScale.z);
            }
            else if (direction < 0)
            {
                transform.localScale = new Vector3(-baseScaleX, transform.localScale.y, transform.localScale.z);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (IsDead) return;
                if (!player.CanBeHit) return;

                Vector2 recoilForce =
                    new Vector2(-collision.relativeVelocity.normalized.x, -collision.relativeVelocity.normalized.y) *
                    500.0f;
                player.Hurt(20, recoilForce);

                OnHitPlayer(collision.relativeVelocity);
            }
        }

        public Animator GetAnimator()
        {
            return animator;
        }

        public void SetAnimator(Animator animator)
        {
            this.animator = animator;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public bool IsPlayerWithin(float range)
        {
            return Vector2.Distance(transform.position, player.transform.position) < range;
        }
    
        private void OnEnable()
        {
            if (IsDead && animator != null)
                animator.SetBool("IsDead", true);
        }
    }
}