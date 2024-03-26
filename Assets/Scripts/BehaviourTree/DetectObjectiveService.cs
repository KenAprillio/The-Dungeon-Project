using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("DungeonGame/EnemyDetectObjective")]
public class DetectObjectiveService : Service
{
    public LayerMask mask = -1;

    public FloatReference range;
    public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);

    private Collider[] colliders;

    public override void Task()
    {
        // Find target
        colliders = Physics.OverlapSphere(transform.position, range.Value, mask, QueryTriggerInteraction.Ignore);
        if (colliders.Length > 0)
        {
            variableToSet.Value = colliders[0].transform;
        }
        else
        {
            variableToSet.Value = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, range.Value);
            if (colliders.Length > 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, range.Value);
            }
        }
    }
}