using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(FSMController fsm);
    void Update(FSMController fsm);
    void Exit(FSMController fsm);

}
