using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AI;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI.Behaviors
{
    class TurnAround:EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            return TaskStatus.Success;
        }
    }
}
