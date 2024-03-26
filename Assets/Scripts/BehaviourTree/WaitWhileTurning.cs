using MBT;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "DungeonGame/Wait While Turning")]
public class WaitWhileTurning : Leaf
{
    public FloatReference WaitTime = new FloatReference(1f);
    public float RandomDeviation = 0f;
    public bool ContinueOnRestart = false;
    private float _timer;

    [Tooltip("Set the target you want to turn to here")]
    public TransformReference Target;
    public TransformReference Self;

    public override void OnEnter()
    {
        if(!ContinueOnRestart)
            _timer = (RandomDeviation == 0f) ? 0f : Random.Range(-RandomDeviation, RandomDeviation);
    }

    public override NodeResult Execute()
    {
        if (_timer >= WaitTime.Value) 
        {
            // Reset timer in case continueOnRestart option is active
            if (!ContinueOnRestart)
                _timer = (RandomDeviation == 0f) ? 0f : Random.Range(-RandomDeviation, RandomDeviation);
            return NodeResult.success;
        }

        Quaternion targetLook = Quaternion.LookRotation(Target.Value.position - Self.Value.position); ;

        Self.Value.transform.rotation = Quaternion.Slerp(transform.rotation, targetLook, 5 * Time.deltaTime);

        _timer += this.DeltaTime;
        return NodeResult.running;
    }

    private void OnValidate()
    {
        if (WaitTime.isConstant)
            RandomDeviation = Mathf.Clamp(RandomDeviation, 0f, WaitTime.GetConstant());
        else
            RandomDeviation = Mathf.Clamp(RandomDeviation, 0f, 600f);
    }
}
