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
        public override NodeResult Execute()
        {
            Agent.SetTrigger("isAttacking");
            return NodeResult.success;
        }
    }
}
