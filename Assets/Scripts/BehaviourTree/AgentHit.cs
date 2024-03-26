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
        public BoolReference isAttackPlayer = new BoolReference(VarRefMode.DisableConstant);

        [Header("Influencing Others")]
        private Collider[] _colliders;
        public LayerMask mask = -1;
        public override void OnEnter()
        {
            // Sets this enemy to attack player
            isAttackPlayer.Value = true;

            // Initiate influence sphere
            _colliders = Physics.OverlapSphere(transform.position, 10, mask, QueryTriggerInteraction.Ignore);

            // Check if there's anyone to influence
            if (_colliders.Length > 0)
            {
                for (int i = 0; i < _colliders.Length; i++)
                {
                    if (_colliders[i].GetComponentInChildren<Blackboard>())
                    {
                        // Influence other enemies in radius to attack player
                        Blackboard blackboard = _colliders[i].GetComponentInChildren<Blackboard>();

                        BoolVariable attackPlayer = blackboard.GetVariable<BoolVariable>("attackPlayer");
                        attackPlayer.Value = true;
                    }
                }
            }
        }

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
