using UnityEngine;
using MBT;
using UnityEngine.AI;

// Empty Menu attribute prevents Node to show up in "Add Component" menu.
[AddComponentMenu("")]
// Register node in visual editor node finder
[MBTNode(name = "DungeonGame/Move Agent to Target")]
public class MoveAgentToTarget : Leaf
{
    public TransformReference Destination;
    public NavMeshAgent Agent;
    public Animator AgentAnimator;
    public float StopDistance = 2f;

    [Tooltip("How often target destination should be updated")]
    public float UpdateInterval = 1f;
    private float _time = 0;

    // These two methods are optional, override only when needed
    // public override void OnAllowInterrupt() {}
    public override void OnEnter() 
    {
        _time = 0;
        Agent.isStopped = false;
        AgentAnimator.SetBool("isWalking", true);
        Agent.SetDestination(Destination.Value.position);
    }

    // This is called every tick as long as node is executed
    public override NodeResult Execute()
    {
        if (AgentAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Agent.isStopped = true;
        }
        else
        {
            Agent.isStopped = false;
        }
        _time += Time.deltaTime;

        // Update destination every given interval
        if (_time > UpdateInterval)
        {
            // Reset time and update destination
            _time = 0;
            Agent.SetDestination(Destination.Value.position);
        }

        // Check if path is ready
        if (Agent.pathPending)
            return NodeResult.running;

        // Check if agent is very close to destination
        if (Agent.remainingDistance < StopDistance)
            return NodeResult.success;
            

        // Check if there is any path (if not pending, it should be set)
        if (Agent.hasPath)
            return NodeResult.running;
            

        return NodeResult.failure;
    }

    // These two methods are optional, override only when needed
    public override void OnExit() 
    {
        AgentAnimator.SetBool("isWalking", false);
        Agent.isStopped = true;
    }
    // public override void OnDisallowInterrupt() {}

    // Usually there is no needed to override this method
    public override bool IsValid()
    {
        return !(Destination.isInvalid || Agent == null);
    }
}