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
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
        Ctx.AppliedMovementY = Ctx.CurrentMovementInput.y;

        Ctx.Animator.SetTrigger("isDashing");
        Ctx.IsDashing = true;
        Ctx.IsAbleToDash = false;
        HandleDash();

    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        Ctx.Rigidbody.velocity = previousVelocity;
        //Ctx.IsDashing = false;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchState()
    {
        Ctx.StartCoroutine(Ctx.DashCooldown());

        if (!Ctx.IsMovementPressed && !Ctx.IsDashing)
        {
            SwitchState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsDashing)
        {
            SwitchState(Factory.Run());
        }

        if (Ctx.IsHit)
        {
            SwitchState(Factory.Hit());
        }
    }

    void HandleDash()
    {
        previousVelocity = Ctx.Rigidbody.velocity;
        Quaternion currentRotation = Ctx.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(Ctx.CameraRelativeDirections, Vector3.up);

        Vector2 absoluteDirection = new Vector2(Mathf.Abs(Ctx.CameraRelativeDirections.x), Mathf.Abs(Ctx.CameraRelativeDirections.y));
        Vector3 dashTo = Ctx.IsMovementPressed ? Ctx.CameraRelativeDirections : Ctx.transform.forward;
        Ctx.Rigidbody.velocity = dashTo * Ctx.DashSpeed;
        Ctx.Rigidbody.rotation = Quaternion.Slerp(currentRotation, targetRotation, Ctx._rotationFactorPerFrame * Time.deltaTime);

        Ctx.StartCoroutine(ResetDash());

    }

    void Dashing()
    {
        previousVelocity = Ctx.Rigidbody.velocity;

        Vector3 forceToApply = Ctx.CameraRelativeDirections * Ctx.DashSpeed;
        delayedForceToApply = forceToApply;

        Ctx.StartCoroutine(DelayedDash());

        Ctx.StartCoroutine(ResetDash());
    }

    Vector3 delayedForceToApply;
    IEnumerator DelayedDash()
    {
        yield return new WaitForSeconds(.025f);
        Ctx.Rigidbody.AddForce(delayedForceToApply, ForceMode.Impulse);
        Debug.Log("DASH!");
    }

    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(Ctx.DashDuration);

        Debug.Log("Is this running?");
        CheckSwitchState();
    }
}
