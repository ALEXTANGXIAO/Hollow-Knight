using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AI;
using Core.Character;
using Core.Combat;
using UnityEngine;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI
{
    public class Shot:EnemyAction
    {
        public bool shakeCamera;

        public Weapon weapon;

        public override TaskStatus OnUpdate()
        {

            var projectile = UnityEngine.Object.Instantiate(weapon.projectilePrefab,
                weapon.weaponTransform.position, Quaternion.identity);
            projectile.Shooter = gameObject;

            var force = new Vector2(weapon.horizontalForce * transform.localScale.x,weapon.verticalForce);
            projectile.SetForce(force);

            if (shakeCamera)
            {
                CameraController.Instance.ShakeCamera(0.5f);   
            }

            return TaskStatus.Success;
        }
    }
}
