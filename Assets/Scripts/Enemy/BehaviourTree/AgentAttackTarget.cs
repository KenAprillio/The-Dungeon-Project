using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("DungeonGame/Agent Attack Target")]
    [AddComponentMenu("")]
    public class AgentAttackTarget : Leaf
    {
        public Animator Agent;
        public TransformReference Target;

        public override void OnEnter()
        {
            Agent.SetTrigger("isAttacking");
        }

        public override NodeResult Execute()
        {
            if(Agent.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && Agent.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                Agent.ResetTrigger("isAttacking");
                return NodeResult.success;
            } else
                return NodeResult.running;
        }
    }
}
