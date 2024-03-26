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

        /*// Determine target direction
        Vector3 targetDirection = target.Value.position - self.Value.position;

        float turnSpeed = payloadManager.TurnSpeed * Time.deltaTime;

        // Rotate object
        Vector3 newDirection = Vector3.RotateTowards(self.Value.forward, targetDirection, turnSpeed, 0f);*/

        //self.Value.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, self.Value.position.y, newDirection.z));
        self.Value.LookAt(new Vector3(target.Value.position.x, self.Value.position.y, target.Value.position.z));
    }

    
}