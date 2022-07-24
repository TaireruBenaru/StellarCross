using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "MemberStats", order = 1)]
public class MemberStats : ScriptableObject
{
   public string FirstName = "";
   public string LastName = "";
   public int BaseLevel = 0;
   public int HPRate = 0;
   public int EPRate = 0;
   public int AttackRate = 0;
   public int MagicRate = 0;
   public int DefenseRate = 0;
   public int ResistanceRate = 0;
   public int SpeedRate = 0;
   public int LuckRate = 0;
   public LevelUpEntry[] SkillList;
   public ElementalAffinity[] eAffinity = new ElementalAffinity[12];
   public StatusAffinity[] sAffinity = new StatusAffinity[12];
   public int ExpRate = 0;
   
}

[Serializable]
public struct LevelUpEntry
{
   public SkillID Skill;
   public int Level;
}

public enum MemberID
{
   None,
   Chiharu,
   Hirari,
   Kaoru,
}