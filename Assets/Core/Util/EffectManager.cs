using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Util
{
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance;

        public GameObject goldPrefab;

        private Transform currentEffectsObject;
        private Transform currentEffectsParent;

        private List<ParticleSystem> effects;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            effects = new List<ParticleSystem>();
        }

        public void PlayOneShot(ParticleSystem particleSystem, Vector3 position)
        {
            if (particleSystem == null) return;
            
            var effect = Instantiate(particleSystem, position, Quaternion.identity);
            effect.Play();

            var duration = effect.main.duration + effect.main.startLifetime.constantMax;
            effect.gameObject.AddComponent<Disposable>().lifetime = duration;
        }

        public void SpawnGold(Vector3 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = Instantiate(goldPrefab, position, Quaternion.identity);
                var xDirection = Random.Range(-1.0f, 1.0f);
                item.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection * 6.0f, Random.Range(12.0f, 20.0f)),
                    ForceMode2D.Impulse);
                item.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-10, 10));
            }
        }

        private class EffectPool
        {
            private const int PoolSize = 5;

            private List<ParticleSystem> effectPool;
            private int currentEffectIndex;

            public EffectPool(ParticleSystem particleSystem)
            {
                var pMain = particleSystem.main;
                pMain.playOnAwake = false;

                effectPool = new List<ParticleSystem>();
                for (int i = 0; i < PoolSize; i++)
                {
                    effectPool.Add(Instantiate(particleSystem, EffectManager.Instance.transform));
                }
            }

            public void Play(Vector3 position)
            {
                var effect = effectPool[currentEffectIndex];
                effect.transform.position = position;
                effect.Play();

                currentEffectIndex = (currentEffectIndex + 1) % effectPool.Count;
            }

            public void PlayWithColor(Vector3 position, Color color)
            {
                var effect = effectPool[currentEffectIndex];

                // Temporarily override start color
                var main = effect.main;
                var prevColor = main.startColor;
                main.startColor = color;

                Play(position);

                EffectManager.Instance.StartCoroutine(ResetEffectColor(main, prevColor, main.duration));
            }

            private IEnumerator ResetEffectColor(ParticleSystem.MainModule system, ParticleSystem.MinMaxGradient color,
                float delay)
            {
                yield return new WaitForSeconds(delay);
                system.startColor = color;
            }
        }
    }
}