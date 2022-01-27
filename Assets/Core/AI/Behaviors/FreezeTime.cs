using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime;
using Core;
using Core.AI;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI.Behaviors
{
    class FreezeTime:EnemyAction
    {
        public SharedFloat Duration = 0.1f;

        public override TaskStatus OnUpdate()
        {
            GameManager.Instance.FreezeTime(Duration.Value);
            return TaskStatus.Success;
        }
    }
}
