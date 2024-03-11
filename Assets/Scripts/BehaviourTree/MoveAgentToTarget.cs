using UnityEngine;
using MBT;
using UnityEngine.AI;

// Empty Menu attribute prevents Node to show up in "Add Component" menu.
[AddComponentMenu("")]
// Register node in visual editor node finder
[MBTNode(name = "DungeonGame/Move Agent to Target")]
public class MoveAgentToTarget : Leaf
{
    public TransformReference destination;
    public NavMeshAgent agent;
    public float stopDistance = 2f;

    [Tooltip("How often target destination should be updated")]
    public float updateInterval = 1f;
    private float time = 0;

    // These two methods are optional, override only when needed
    // public override void OnAllowInterrupt() {}
    public override void OnEnter() 
    {
        time = 0;
        agent.isStopped = false;
        agent.SetDestination(destination.Value.position);
    }

    // This is called every tick as long as node is executed
    public override NodeResult Execute()
    {
        time += Time.deltaTime;

        // Update destination every given interval
        if (time > updateInterval)
        {
            // Reset time and update destination
            time = 0;
            agent.SetDestination(destination.Value.position);
        }

        // Check if path is ready
        if (agent.pathPending)
            return NodeResult.running;

        // Check if agent is very close to destination
        if (agent.remainingDistance < stopDistance)
            return NodeResult.success;

        // Check if there is any path (if not pending, it should be set)
        if (agent.hasPath)
            return NodeResult.running;

        return NodeResult.failure;
    }

    // These two methods are optional, override only when needed
    public override void OnExit() 
    {
        agent.isStopped = true;
    }
    // public override void OnDisallowInterrupt() {}

    // Usually there is no needed to override this method
    public override bool IsValid()
    {
        return !(destination.isInvalid || agent == null);
    }
}