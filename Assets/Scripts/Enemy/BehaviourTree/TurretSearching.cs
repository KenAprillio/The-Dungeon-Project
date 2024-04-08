using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("DungeonGame/Turret Searching")]
    [AddComponentMenu("")]
    public class TurretSearching : Leaf
    {
        public Animator payload;
        public TransformReference target;
        public override NodeResult Execute()
        {
            if (target.Value)
            {
                payload.SetBool("isSearching", false);

                return NodeResult.success;
            }
            else
            {
                payload.SetBool("isSearching", true);
                return NodeResult.running;
            }
        }
    }
}
