using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "BattleEnemies", order = 1)]
public class BattleEnemies : ScriptableObject
{
   public string Name = "";
   public AIFunction AI; 
   public int Level = 0;
   public int HP = 0;
   public int EP = 0;
   public SkillID[] Skills = new SkillID[8];
   public int[] SkillUseChance = new int[8];
   public int Attack = 0;
   public int Magic = 0;
   public int Defense = 0;
   public int Resistance = 0;
   public int Speed = 0;
   public int Luck = 0;
   public int EXPReward = 0;
   public ElementalAffinity[] eAffinity = new ElementalAffinity[12];
   public StatusAffinity[] sAffinity = new StatusAffinity[12];
}

public enum SkillAffinity {Neutral, Weak, Resist, Block, Repel};

[Serializable]
public struct ElementalAffinity
{
   public SkillElement Element;
   public SkillAffinity Affinity;
}

[Serializable]
public struct StatusAffinity
{
   public SkillElement Status;
   public SkillAffinity Affinity;
}

public enum EnemyID
{
   None,
   TestEnemy
}

public enum AIFunction
{
   AI_Generic
}