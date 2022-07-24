using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Generic : EnemyAI
{
    
    public override IEnumerator ActStart()
    {
        ChosenSkill = Self.Data.Skills[0];
        for (int i = 0; i < Self.Data.SkillUseChance.Length; i++)
        {
            if(UnityEngine.Random.Range(0, 101) < Self.Data.SkillUseChance[i])
            {
                ChosenSkill = Self.Data.Skills[i];
                Debug.Log("Chosen skill = " + ChosenSkill);
            }
        }
        TargetRandomPlayer();
        yield return null;
    }
}
