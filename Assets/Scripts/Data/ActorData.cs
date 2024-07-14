using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorData 
{
    public string actorName;
    public string iconPath;
    public int hP;
    public int mp;
}
public class NormarMonsterData : ActorData
{

}
public class BossMonsterData : ActorData
{

}
public class NormarTowerData : ActorData
{

}
public class ChampionTowerData : ActorData
{

}
