using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AI;
using Core.Combat.Projectiles;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI.Behaviors
{
    class SpawnFallingRocks:EnemyAction
    {
        public Collider2D spawnAreaCollider;
        public AbstractProjectile rockPrefab;
        public int spawnCount = 4;
        public float spawnInterval = 0.3f;
        public override TaskStatus OnUpdate()
        {
            var sequence = DOTween.Sequence();
            for (int i = 0; i < spawnCount; i++)
            {
                sequence.AppendCallback(SpawnRock);
                sequence.AppendInterval(spawnInterval);
            }

            return TaskStatus.Success;
        }

        private void SpawnRock()
        {
            var randomX = UnityEngine.Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
            var rock = UnityEngine.Object.Instantiate(rockPrefab, new Vector3(randomX, spawnAreaCollider.bounds.min.y),
                Quaternion.identity);
            rock.SetForce(Vector2.zero);
        }
    }
}
