using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDataManager
{
    private static DamageDataManager _instance;
    public static DamageDataManager instance
    {
        get
        {
            if( _instance == null )
            {
                _instance = new DamageDataManager();
            }
            return _instance;
        }
    }
}