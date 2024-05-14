using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }


    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    public override void EnterState()
    {
        Ctx.Animator.SetTrigger("isHit");
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

    }

    public override void InitializeSubState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f && Ctx.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damaged"))
        {
            Debug.Log("Im Hit!");
            Ctx.Animator.ResetTrigger("isHit");
            Ctx.IsHit = false;

            if (Ctx.IsDead)
            {
                SwitchState(Factory.Death());
            }
            else
            {
                if (!Ctx.IsMovementPressed)
                {
                    SwitchState(Factory.Idle());
                }
                else if (Ctx.IsMovementPressed)
                {
                    SwitchState(Factory.Run());
                }

                if (Ctx.IsDashPressed && Ctx.IsAbleToDash)
                {
                    SwitchState(Factory.Dash());
                }

                if (Ctx.IsAttackPressed)
                {
                    SwitchState(Factory.Attack());
                }
            }
        }

        

    }
}
