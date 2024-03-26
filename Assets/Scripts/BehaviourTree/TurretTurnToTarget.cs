using MBT;
using Unity.VisualScripting;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("DungeonGame/TurretTurnToTarget")]
public class TurretTurnToTarget : Service
{

    public PayloadAttackManager payloadManager;
    public TransformReference self;
    public TransformReference target;
    public Animator payloadAnimator;

    public override void Task()
    {
        if(payloadAnimator)
            payloadAnimator.SetBool("isSearching", false);

        // Look at enemy
        self.Value.LookAt(new Vector3(target.Value.position.x, self.Value.position.y, target.Value.position.z));
    }

    
}