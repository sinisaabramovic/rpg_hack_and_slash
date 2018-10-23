using UnityEngine;
using States;

public class FirstState : State<AIFSM>
{
    private static FirstState _instance;

    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstState();
            }

            return _instance;
        }
    }

    public override void EnterState(AIFSM _owner)
    {
        Debug.Log("Entering First State !!!!");
    }

    public override void ExitState(AIFSM _owner)
    {
        Debug.Log("Exiting First State !!!!");
    }

    public override void UpdateState(AIFSM _owner)
    {
        if (_owner.switchState)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
}
