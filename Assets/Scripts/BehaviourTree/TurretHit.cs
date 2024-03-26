using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("DungeonGame/Turret Hit")]
    [AddComponentMenu("")]
    public class TurretHit : Leaf
    {
        public Animator payload;
        public PayloadHealthManager payloadHealth;
        public FloatReference damageReceived = new FloatReference(VarRefMode.DisableConstant);
        public BoolReference isHitToSet = new BoolReference(VarRefMode.DisableConstant);
        public override NodeResult Execute()
        {
            Debug.Log("Payload is Hit!!!!");
            //payload.SetTrigger("isHit");

            payloadHealth.PayloadHit(damageReceived.Value);

            isHitToSet.Value = false;
            damageReceived.Value = 0;

            /*if (payload.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f && payload.GetCurrentAnimatorStateInfo(0).IsTag("Hit"))
            {
                payload.ResetTrigger("isHit");

            }*/
            return NodeResult.success;
        }
    }
}
