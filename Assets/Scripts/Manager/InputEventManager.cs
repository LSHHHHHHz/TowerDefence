using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventManager
{
    public Action enableMove;
    public Action disableMove;
    public void UpdateInput()
    {
        if (Input.GetButtonDown("button1"))
        {
            enableMove?.Invoke();
        }
        if (Input.GetButtonDown("button2"))
        {
            disableMove?.Invoke();
        }
    }
}
