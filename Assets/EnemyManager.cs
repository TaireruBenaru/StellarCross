using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    [Header("Data")]
    public BattleUnits[] EncounterTBL;
    public BattleEnemies[] EnemyTBL;

    [Header("Other")]
    public int BattleEncounterID;
    public int EnemyPartySize {get {return EncounterTBL[BattleEncounterID].EnemyUnit.Length; }}

    public static EnemyManager Instance = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum EncounterIDs
    {
        Test
    }
}
