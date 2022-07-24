using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Battlebox : MonoBehaviour
{
    public int Character;
    public int CharacterID;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI EP;
    public TextMeshProUGUI SP;
    public Color[] HPColor;
    public Color[] RPColor;

    public GameObject OPBar;
    public GameObject HPBar;
    public GameObject RPBar;

    public int HPDecrease;
    public int RPDecrease;
    public int Speed;
    public int Wait;
    int frame;

    // Start is called before the first frame update
    void Start()
    {
        CharacterID = PartyManager.Instance.CurrentParty[Character];
    }

    // Update is called once per frame
    void Update()
    {
        NameText.text = PartyManager.Instance.MemberStats[CharacterID].FirstName;
        HP.text = PartyManager.Instance.MemberStats[CharacterID].CurrentHP.ToString("D" + GetIntegerDigitCountString(PartyManager.Instance.MemberStats[CharacterID].MaxHP));
        EP.text = PartyManager.Instance.MemberStats[CharacterID].CurrentEP.ToString("D" + GetIntegerDigitCountString(PartyManager.Instance.MemberStats[CharacterID].MaxEP));
    }

    static int GetIntegerDigitCountString(int value)
    {
        return value.ToString().Length;
    }
}
