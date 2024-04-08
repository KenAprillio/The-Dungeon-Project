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
        public override NodeResult Execute()
        {
            //Agent.SetTrigger("isAttacking");
            Debug.Log("Im Shooting!");
            return NodeResult.success;
        }
    }
}
