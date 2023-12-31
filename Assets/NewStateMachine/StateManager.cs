using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState , BaseState<EState>> States = new Dictionary<EState, BaseState<EState>> ();

    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;
    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        } else if(!IsTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }

    public void TransitionToState(EState statekey)
    {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[statekey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
