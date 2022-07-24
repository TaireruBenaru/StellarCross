using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Skills", order = 1)]
public class Skills : ScriptableObject
{
   public string Name = "";
   [TextArea(3, 4)] public string Description = "";

   public SkillRange Range = SkillRange.OneEnemy;
   
   public SkillEffects Effect = SkillEffects.Damage;
   public int Power = 0;
   public int InflictChance = 0;
   public int EP = 0;
   public int MinHits = 1;
   public int MaxHits = 1;
   
   public BuffOrDebuffs BuffOrDebuff = BuffOrDebuffs.None;
   public int BuffOrDebuffChance = 0;
   public SkillTypes Type = SkillTypes.Attack;
   public SkillElement Element;
   public Status InflictStatusAilment = Status.None;
   public bool isMagical = false;
}

public enum SkillRange {OneEnemy, AllEnemies, AOEEnemy, OneAlly, AllAllies};
public enum SkillTypes {Attack, Recovery, Support};
public enum BuffOrDebuffs {None, AttackUp1, AttackUp2, AttackDown1, AttackDown2, DefenseUp1, DefenseUp2, DefenseDown1, DefenseDown2, SpeedUp1, SpeedUp2, SpeedDown1, SpeedDown2, AllUp1, AllUp2, AllDown1, AllDown2};
public enum SkillEffects {Damage, Heal, ReduceHPTo1, ExactDamage, ExactHeal, DrainHP, DrainEP, EscapeBattle, HealAllHP, HealAllHPAndEP, HealAllEP, WeaponAttack};
public enum SkillElement {Slash, Impact, Pierce, Fire, Ice, Spark, Light, Dark, Neutral, Heart, Support, Ailment};
public enum Status {None, Poison, Paralysis, Freeze, Burn, Sleep, Rage, Offline};

public enum SkillID
{
   None,
   Espada,
   DisparoElectrico,
   Lunge,
   Badine,
}