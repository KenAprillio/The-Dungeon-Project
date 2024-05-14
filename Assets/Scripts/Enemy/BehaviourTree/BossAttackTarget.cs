using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("DungeonGame/Boss Attack Target")]
    [AddComponentMenu("")]
    public class BossAttackTarget : Leaf
    {
        public Animator Agent;
        public TransformReference Target;
        bool _executeSpecialAttack;

        public override void OnEnter()
        {
            _executeSpecialAttack = (Random.Range(0, 2) == 0);
            if(_executeSpecialAttack)
                Agent.SetTrigger("isAttacking2");
            else
                Agent.SetTrigger("isAttacking");

        }

        public override NodeResult Execute()
        {
            if (Agent.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && Agent.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                Agent.ResetTrigger("isAttacking");
                Agent.ResetTrigger("isAttacking2");
                return NodeResult.success;
            }
            else
                return NodeResult.running;
        }
    }
}
