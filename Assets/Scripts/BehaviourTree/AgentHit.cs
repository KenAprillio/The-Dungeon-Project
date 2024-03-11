using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("DungeonGame/Agent Hit")]
    [AddComponentMenu("")]
    public class AgentHit : Leaf
    {
        public Animator agent;
        public EnemyHealthManager agentHealth;
        public FloatReference damageReceived = new FloatReference(VarRefMode.DisableConstant);
        public BoolReference isHitToSet = new BoolReference(VarRefMode.DisableConstant);
        public override NodeResult Execute()
        {
            Debug.Log("Enemy is Hit!!!!");
            agent.SetTrigger("isHit");

            agentHealth.EnemyHit(damageReceived.Value);

            isHitToSet.Value = false;
            damageReceived.Value = 0;

            if (agent.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f && agent.GetCurrentAnimatorStateInfo(0).IsTag("Hit"))
            {
                agent.ResetTrigger("isHit");

                return NodeResult.success;
            }

            return NodeResult.running;
        }
    }
}
