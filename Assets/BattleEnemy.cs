using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : MonoBehaviour
{
    [Header("System")]
    public int Enemy;
    int EnemyID;
    public int ATBGauge;
    public BattleEnemies Data;
    public EnemyAI AICALC;

    //[Header("Stats")]
    public int Level { get { return Data.Level; } }
    public int MaxHP { get { return Data.HP; } }
    public int CurrentHP;
    public int MaxEP { get { return Data.EP; } }
    public int CurrentEP;
    public int CurrentAttack { get { return Data.Attack; } }
    public int CurrentMagic { get { return Data.Magic; } }
    public int CurrentDefense { get { return Data.Defense; } }
    public int CurrentResistance { get { return Data.Resistance; } }
    public int CurrentSpeed { get { return Data.Speed; } }
    public int CurrentLuck { get { return Data.Luck; } }
    // Start is called before the first frame update
    void Start()
    {
        EnemyID = (int)EnemyManager.Instance.EncounterTBL[BattleManager.Instance.EncounterID].EnemyUnit[Enemy];
        Data = EnemyManager.Instance.EnemyTBL[EnemyID];
        gameObject.AddComponent(System.Type.GetType(Data.AI.ToString()));
        AICALC = GetComponent<EnemyAI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tick()
    {
        ATBGauge += 100 + CurrentSpeed;
        ATBGauge = Mathf.Clamp(ATBGauge, 0, 50000);
    }

    public IEnumerator Act()
    {
        yield return StartCoroutine(AICALC.ActStart());
        yield return null;
    }
}
