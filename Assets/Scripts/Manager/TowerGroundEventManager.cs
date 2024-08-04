using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundEventManager
{
    public Action<TowerGround> onMouseEnter;
    public Action<TowerGround> onMouseExit;
    public Action towerOnGround;
    public Action towerOutGround;
    public void MouseEnter(TowerGround tower)
    {
        onMouseEnter?.Invoke(tower);
    }
    public void MouseExit(TowerGround tower)
    {
        onMouseExit?.Invoke(tower);
    }
    public void TowerOnGround()
    {
        towerOnGround?.Invoke();
    }
    public void TowerOutGround()
    {
        towerOutGround?.Invoke();
    }
}
