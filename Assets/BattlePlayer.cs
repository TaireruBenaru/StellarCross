using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    [Header("System")]
    public int Character;
    int CharacterID;
    public int ATBGauge;

    //[Header("Stats")]
    public int Level { get { return PartyManager.Instance.MemberStats[CharacterID].Level; } }
    public int MaxHP { get { return PartyManager.Instance.MemberStats[CharacterID].MaxHP; } }
    public int CurrentHP { get { return PartyManager.Instance.MemberStats[CharacterID].CurrentHP; } }
    public int MaxEP { get { return PartyManager.Instance.MemberStats[CharacterID].MaxEP; } }
    public int CurrentEP { get { return PartyManager.Instance.MemberStats[CharacterID].CurrentEP; } }
    public int CurrentAttack { get { return PartyManager.Instance.MemberStats[CharacterID].Attack; } }
    public int CurrentMagic { get { return PartyManager.Instance.MemberStats[CharacterID].Magic; } }
    public int CurrentDefense { get { return PartyManager.Instance.MemberStats[CharacterID].Defense; } }
    public int CurrentResistance { get { return PartyManager.Instance.MemberStats[CharacterID].Resistance; } }
    public int CurrentSpeed { get { return PartyManager.Instance.MemberStats[CharacterID].Speed; } }
    public int CurrentLuck { get { return PartyManager.Instance.MemberStats[CharacterID].Luck; } }
    // Start is called before the first frame update
    void Start()
    {
        CharacterID = PartyManager.Instance.CurrentParty[Character];
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
        yield return StartCoroutine(BattleCommand.Instance.CommandWheel());
    }
}
