using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "BattleUnits", order = 1)]
public class BattleUnits : ScriptableObject
{
   public EnemyID[] EnemyUnit = new EnemyID[5];
   public bool CanEscape = true;
}