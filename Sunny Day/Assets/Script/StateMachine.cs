using UnityEngine;
using System.Collections.Generic;

public class StateMachine
{
    private IState _currentState;
    private Dictionary<System.Type, IState> _states = new Dictionary<System.Type, IState>();


    public IState CurrentState => _currentState;

    public void AddState(IState state)
    {
        _states[state.GetType()] = state;
    }

    public void ChangeState<T>() where T : IState
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = _states[typeof(T)];
        _currentState.Enter();
    }

    public void ExitCurrentState()
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState = null;
        }
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
}
