using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
            IsRootState = true;
            InitializeSubState();
        }

    public override void EnterState()
    {
        //Debug.Log("Iam currently grounded");
    }

    public override void UpdateState() { 
        CheckSwitchState();
    }
    public override void FixedUpdateState()
    {

    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        } else if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Run());
        } else if (Ctx.IsAbleToDash && Ctx.IsDashPressed)
        {
            SetSubState(Factory.Dash());
        } else if (Ctx.CurrentAttack > 0)
        {
            SetSubState(Factory.Attack());
        }
    }

    public override void CheckSwitchState()
    {
    }
}
