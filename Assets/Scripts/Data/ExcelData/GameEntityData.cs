using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class GameEntityData : ScriptableObject
{
	public List<ProfileDB> profileEntity;
	public List<ActorStatusDB> actorStatusEntity; 
}
