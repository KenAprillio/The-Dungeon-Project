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
        public Animator agent;
        public override NodeResult Execute()
        {
            agent.SetTrigger("isAttacking");
            return NodeResult.success;
        }
    }
}
