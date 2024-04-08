using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }
    public override void EnterState() { 
        Ctx.Animator.SetBool("isWalking", true);
        //Debug.Log("I am Running!");
    }

    public override void UpdateState() {
        CheckSwitchState();
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
        Ctx.AppliedMovementY = Ctx.CurrentMovementInput.y;
    }
    public override void FixedUpdateState()
    {

    }

    public override void ExitState()
    {
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementY = 0;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchState() {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        
        if (Ctx.IsDashPressed && Ctx.IsAbleToDash)
        {
            SwitchState(Factory.Dash());
        }

        if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }

        if (Ctx.IsHit)
            SwitchState(Factory.Hit());
    }
}
