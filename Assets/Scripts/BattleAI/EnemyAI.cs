using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public int Target;
    public SkillID ChosenSkill;

    public BattleEnemy Self;

    // Start is called before the first frame update
    void Start()
    {
        Self = GetComponent<BattleEnemy>();
    }
    // Called when the battle starts
    public virtual IEnumerator BattleStart()
    {
        yield return null;
    }

    //Called when it's the enemy's turn
    public virtual IEnumerator ActStart()
    {
        yield return null;
    }

    //Called at the end of battle.
    public virtual IEnumerator BattleEnd()
    {
        yield return null;
    }

    //Called at the end of the enemy's turn
    public virtual IEnumerator ActEnd()
    {
        yield return null;
    }

    public void TargetRandomPlayer()
    {
        Target = UnityEngine.Random.Range(0, PartyManager.Instance.PartySize);
    }
}
