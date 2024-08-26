using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> where T : Actor
{
    void Enter(T actor);
    void Update(T actor);
    void Exit(T actor);
}
