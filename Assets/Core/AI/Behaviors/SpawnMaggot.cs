using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AI;
using Core.Combat;
using UnityEngine;
using Object = System.Object;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI.Behaviors
{
    public class SpawnMaggot:EnemyAction
    {
        public GameObject maggotPerfab;
        public Transform maggotTransform;
        public GameObject hazardCollider;


        private Destructable maggot;

        public override void OnStart()
        {
            base.OnStart();
            maggot = UnityEngine.Object.Instantiate(maggotPerfab, maggotTransform).GetComponent<Destructable>();
            maggot.transform.localPosition = Vector3.zero;
            destructable.Invincible = true;
            hazardCollider.SetActive(false);
        }

        public override TaskStatus OnUpdate()
        {
            if (maggot.CurrentHealth>0)
            {
                return TaskStatus.Running;
            }

            destructable.Invincible = false;

            hazardCollider.SetActive(true);

            return TaskStatus.Success;
        }
    }
}
