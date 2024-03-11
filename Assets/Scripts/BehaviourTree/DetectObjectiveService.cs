using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("DungeonGame/EnemyDetectObjective")]
public class DetectObjectiveService : Service
{
    public LayerMask mask = -1;

    public float range = 15;
    public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);

    public override void Task()
    {
        // Find target
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask, QueryTriggerInteraction.Ignore);
        if (colliders.Length > 0)
        {
            variableToSet.Value = colliders[0].transform;
        }
        else
        {
            variableToSet.Value = null;
        }
    }
}