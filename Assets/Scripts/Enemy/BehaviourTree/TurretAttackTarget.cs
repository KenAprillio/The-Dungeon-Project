using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("DungeonGame/Turret Attack Target")]
    [AddComponentMenu("")]
    public class TurretAttackTarget : Leaf
    {
        public Transform shootSource;
        public Animator AgentAnimator;
        public override NodeResult Execute()
        {
            AgentAnimator.SetTrigger("isAttacking");
            Debug.Log("Im Shooting!");
            return NodeResult.success;
        }
    }
}
