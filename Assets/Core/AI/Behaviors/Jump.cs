using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using Core.Character;
using DG.Tweening;
using UnityEngine;

public class Jump : EnemyAction
{
    public float horizontalForce = 5.0f;
    public float jumpForce = 10f;

    public float buildupTime;
    public float jumpTime;

    public string animationTrigerName;
    public bool shakeCameraOnLoading;

    private bool hasLanded;

    private Tween buildupTween;
    private Tween jumpTween;
    public override void OnStart()
    {
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
        animator.SetTrigger(animationTrigerName);
    }

    private void StartJump()
    {
        var direction = player.transform.position.x < transform.position.x ? -1 : 1;

        body.AddForce(new Vector2(horizontalForce*direction,jumpForce),ForceMode2D.Impulse);

        jumpTween = DOVirtual.DelayedCall(jumpTime, (() =>
        {
            hasLanded = true;
            if (shakeCameraOnLoading)
            {
                CameraController.Instance.ShakeCamera(0.5f);
            }
        }),false);
    }

    public override TaskStatus OnUpdate()
    {
        return hasLanded ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        buildupTween?.Kill();
        jumpTween?.Kill();
        hasLanded = false;
    }
}
