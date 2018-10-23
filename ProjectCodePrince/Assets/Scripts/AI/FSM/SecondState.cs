using UnityEngine;
using States;

public class SecondState : State<AIFSM>
{
    private static SecondState _instance;

    private SecondState(){
        if(_instance != null){
            return;
        }

        _instance = this;
    }

    public static SecondState Instance{
        get{
            if(_instance == null){
                new SecondState();
            }

            return _instance;
        }
    }

    public override void EnterState(AIFSM _owner)
    {
        Debug.Log("Entering Second State !!!!");
    }

    public override void ExitState(AIFSM _owner)
    {
        Debug.Log("Exiting Second State !!!!");
    }

    public override void UpdateState(AIFSM _owner)
    {
        if(_owner.switchState){
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
    }
}
