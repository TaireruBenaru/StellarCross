using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public MemberStats[] MemberData;
    public PartyMember[] MemberStats = new PartyMember[10];
    public List<int> CurrentParty = new List<int> {1};
    public int PartySize { get { return CurrentParty.Count;} }

    public static PartyManager Instance = null;

    void Awake()
    {
        if (Instance != null) // meaning there's already an instance
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MemberData.Length; i++)
        {
            InitializeMember(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public struct PartyMember
    {
        public string FirstName;
        public string LastName;

        public int Index;

        public int Level;

        public int MaxHP;
        public int CurrentHP;
        public int MaxEP;
        public int CurrentEP;
        public int Attack;
        public int Magic;
        public int Defense;
        public int Resistance;
        public int Speed;
        public int Luck;

        public SkillList[] LearnedSkills;
        public ElementalAffinity[] eAffinity;
        public StatusAffinity[] sAffinity;

    }

    void InitializeMember(int Number)
    {
        MemberStats[Number].Index = Number;

        MemberStats[Number].FirstName = MemberData[Number].FirstName;
        MemberStats[Number].LastName = MemberData[Number].LastName;

        MemberStats[Number].Level = MemberData[Number].BaseLevel;
        MemberStats[Number].eAffinity = MemberData[Number].eAffinity;
        MemberStats[Number].sAffinity = MemberData[Number].sAffinity;

        MemberStats[Number].MaxHP = CalculateHP(MemberData[Number].HPRate, MemberStats[Number].Level);
        MemberStats[Number].CurrentHP = MemberStats[Number].MaxHP;
        MemberStats[Number].MaxEP = CalculateEP(MemberData[Number].EPRate, MemberStats[Number].Level);
        MemberStats[Number].CurrentEP = MemberStats[Number].MaxEP;

        MemberStats[Number].Attack = CalculateStat(MemberData[Number].AttackRate, MemberStats[Number].Level);
        MemberStats[Number].Magic = CalculateStat(MemberData[Number].MagicRate, MemberStats[Number].Level);
        MemberStats[Number].Defense = CalculateStat(MemberData[Number].DefenseRate, MemberStats[Number].Level);
        MemberStats[Number].Resistance = CalculateStat(MemberData[Number].ResistanceRate, MemberStats[Number].Level);
        MemberStats[Number].Speed = CalculateStat(MemberData[Number].SpeedRate, MemberStats[Number].Level);
        MemberStats[Number].Luck = CalculateStat(MemberData[Number].LuckRate, MemberStats[Number].Level);

        MemberStats[Number].LearnedSkills = new SkillList[3];
        for (int i = 0; i < 3; i++)
        {
            MemberStats[Number].LearnedSkills[i].Skills = new List<SkillID>();
        }

        LearnLevelUpSkills(Number);

    }
    
    public void LearnLevelUpSkills(int Member)
    {
        for (int i = 0; i < MemberData[Member].SkillList.Length; i++)
        {
            if(MemberData[Member].SkillList[i].Level <= MemberStats[Member].Level)
            {
                SkillID Skill = MemberData[Member].SkillList[i].Skill;
                int Category = (int)SkillManager.Instance.SkillData[(int)Skill].Type;
                
                int index = MemberStats[Member].LearnedSkills[Category].Skills.IndexOf(Skill);
                if(index == -1)
                {
                    MemberStats[Member].LearnedSkills[Category].Skills.Add(Skill);
                }
            }
        }
        
    }

    public int CalculateHP(int Rate, int Level)
    {
        int NewHP = (Level + Rate) * 13 + UnityEngine.Random.Range(0, 10);
        return NewHP;
    }

    public int CalculateEP(int Rate, int Level)
    {
        int NewEP = (Level * 9) + (Rate * 8) + UnityEngine.Random.Range(0, 5);
        return NewEP;
    }

    public int CalculateStat(int Rate, int Level)
    {
        int BaseStatGain = Level / 5;
        int NewStat = 2 * ((BaseStatGain + Rate * Level) / 10) + UnityEngine.Random.Range(0, 2);
        return NewStat;
    }

    [Serializable]
    public struct SkillList
    {
        public List<SkillID> Skills;
    }
}
