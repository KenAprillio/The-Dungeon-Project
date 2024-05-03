using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }
    Vector3 previousVelocity;
    public override void EnterState()
    {
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementY = 0;
        Ctx.Animator.SetTrigger("isDashing");
        Ctx.IsDashing = true;
        Ctx.IsAbleToDash = false;
        HandleDash();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        previousVelocity = new Vector3(0,0,0);
        Ctx.Rigidbody.velocity = previousVelocity;
        Ctx.IsDashing = false;

    }

    public override void InitializeSubState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsDashing)
        {
            SwitchState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsDashing)
        {
            SwitchState(Factory.Run());
        }
    }

    void HandleDash()
    {
        previousVelocity = Ctx.Rigidbody.velocity;
        Quaternion currentRotation = Ctx.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(Ctx.CameraRelativeDirections, Vector3.up);
        Ctx.Rigidbody.velocity = Ctx.transform.forward * Ctx.DashSpeed;
        Ctx.Rigidbody.rotation = Quaternion.Slerp(currentRotation, targetRotation, Ctx._rotationFactorPerFrame * Time.deltaTime);
    }
}
