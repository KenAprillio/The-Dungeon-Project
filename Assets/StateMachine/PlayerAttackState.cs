using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }


    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    public override void EnterState()
    {
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementY = 0;
        Ctx.IsAttacking = true;

    }

    public override void UpdateState()
    {
        if (Ctx.IsAttackPressed)
        {
            Attack();
        }
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
        if (Ctx.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f && Ctx.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Debug.Log("Reset Attack!");
            comboCounter = 0;
            lastComboEnd = Time.time;
            Ctx.Animator.ResetTrigger("isAttacking1");

            if (!Ctx.IsMovementPressed && !Ctx.IsDashing)
            {
                SwitchState(Factory.Idle());
            }
            else if (Ctx.IsMovementPressed && !Ctx.IsDashing)
            {
                SwitchState(Factory.Run());
            }
        }

        if (Ctx.IsDashPressed && Ctx.IsAbleToDash)
        {
            SwitchState(Factory.Dash());
        }
        
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > .5f && comboCounter < Ctx.ComboList.Count)
        {
            if (Time.time - lastClickedTime >= .3f)
            {
                Ctx.AppliedMovementX = 0;
                Ctx.AppliedMovementY = 0;
                Ctx.Animator.runtimeAnimatorController = Ctx.ComboList[comboCounter].animatorOV;
                Ctx.Animator.Play("Attack", 0, 0);
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter > Ctx.ComboList.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    IEnumerator EndCombo()
    {
        yield return new WaitForSeconds(1f);
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
