using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(Actor actor);
    void Update(Actor actor);
    void Exit(Actor actor);

}
