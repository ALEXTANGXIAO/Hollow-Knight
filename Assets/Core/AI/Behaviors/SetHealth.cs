using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime;
using Core.AI;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI.Behaviors
{
    class SetHealth:EnemyAction
    {
        public SharedInt Health;

        public override TaskStatus OnUpdate()
        {
            destructable.CurrentHealth = Health.Value;
            return TaskStatus.Success;
        }
    }
}
