using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleManager : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineVirtualCamera MainCamera;
    public CinemachineVirtualCamera ArcShotCamera;
    public CinemachineVirtualCamera CloseUpArcShotCamera;
    public CinemachineVirtualCamera PanShotCamera;
    public CinemachineVirtualCamera EnemyPanShotCamera;
    public Animator CameraState;

    [Header("Prefabs")]
    public GameObject BattleBoxPrefab;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    [Header("BattleObjects")]
    public Battlebox[] BattleBoxes;
    public BattlePlayer[] PlayerUnits;
    public BattleEnemy[] EnemyUnits;

    [Header("Variables")]
    public int Turn;

    public const int ATBMax = 50000;

    public int EncounterID {get { return EnemyManager.Instance.BattleEncounterID;}}

    public static BattleManager Instance = null;

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
    IEnumerator Start()
    {
        BattleBoxes = new Battlebox[PartyManager.Instance.PartySize];
        PlayerUnits = new BattlePlayer[PartyManager.Instance.PartySize];
        EnemyUnits = new BattleEnemy[EnemyManager.Instance.EnemyPartySize];

        for (int i = 0; i < PartyManager.Instance.PartySize; i++)
        {
            SetupPartyMember(i);
        }

        for (int j = 0; j < EnemyManager.Instance.EnemyPartySize; j++)
        {
            SetupEnemy(j);
        }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(MainBattleLoop());

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupPartyMember(int Member)
    {
        float XPos = 0;
        float[] Xpos2 = new float[] { -67.76f, 72.76f};
        float[] Xpos3 = new float[] { -134.76f, 5.76f, 145.76f};

        float XPosF = 0;
        float[] XposF2 = new float[] { -0.5f, 0.5f};
        float[] XposF3 = new float[] { 0f, 1f, -1f};

        switch(PartyManager.Instance.PartySize)
        {
            case 1:
                XPos = Xpos3[1];
                XPosF = XposF3[0];
            break;
            case 2:
                XPos = Xpos2[Member];
                XPosF = XposF2[Member];
            break;
            case 3:
                XPos = Xpos3[Member];
                XPosF = XposF3[Member];
            break;
        }

        BattleBoxes[Member] = Instantiate(BattleBoxPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("BattleUI").transform).GetComponent<Battlebox>();
        BattleBoxes[Member].transform.localPosition = new Vector3(XPos,-95f);
        BattleBoxes[Member].Character = Member;

        PlayerUnits[Member] = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<BattlePlayer>();
        PlayerUnits[Member].transform.localPosition = new Vector3(XPosF, 0.2f, 2f);
        PlayerUnits[Member].Character = Member;

        PlayerUnits[Member].ATBGauge = UnityEngine.Random.Range(0, ATBMax);

    }

    void SetupEnemy(int Member)
    {
        float XPos = 0;
        float[] Xpos2 = new float[] { -0.5f, 0.5f};
        float[] Xpos3 = new float[] { 0f, 1f, -1f};
        float[] Xpos4 = new float[] { -134.76f, 5.76f, 145.76f, 145.76f};
        float[] Xpos5 = new float[] { -134.76f, 5.76f, 145.76f, 5.76f, 145.76f};

        switch(EnemyManager.Instance.EnemyPartySize)
        {
            case 1:
                XPos = Xpos3[0];
            break;
            case 2:
                XPos = Xpos2[Member];
            break;
            case 3:
                XPos = Xpos3[Member];
            break;
            case 4:
                XPos = Xpos4[Member];
            break;
            case 5:
                XPos = Xpos5[Member];
            break;
        }

        EnemyUnits[Member] = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity).GetComponent<BattleEnemy>();
        EnemyUnits[Member].transform.localPosition = new Vector3(XPos, 0.2f, 0f);
        EnemyUnits[Member].Enemy = Member;
        EnemyUnits[Member].ATBGauge = UnityEngine.Random.Range(0, ATBMax);

    }

    IEnumerator MainBattleLoop()
    {
        Queue<int> EnemiesActed = new Queue<int>();
        Queue<int> PlayersActed = new Queue<int>();

        CameraState.Play("Arc");

        while (true)
        {
            

            for (int i = 0; i < EnemyUnits.Length; i++)
            {
                EnemyUnits[i].Tick();
                if(EnemyUnits[i].ATBGauge >= ATBMax)
                {
                    Debug.Log("Enemy " + i + " can act.");
                    yield return StartCoroutine(EnemyUnits[i].Act());
                    EnemyPanShotCamera.LookAt = EnemyUnits[i].transform;
                    CameraState.Play("EnemyPan");
                    
                    yield return StartCoroutine(UseSkill(EnemyUnits[i].AICALC.ChosenSkill, i, false, EnemyUnits[i].AICALC.Target, true));
                    EnemyUnits[i].ATBGauge = 0;

                    if(!EnemiesActed.Contains(i))
                    {
                        EnemiesActed.Enqueue(i);
                    }
                    Debug.Log(EnemiesActed.Count);
                }
            }

            //Check deaths n shit
            CameraState.Play("Arc");

            for (int j = 0; j < PlayerUnits.Length; j++)
            {
                PlayerUnits[j].Tick();
                if(PlayerUnits[j].ATBGauge >= ATBMax)
                {
                    Debug.Log("Player " + j + " can act.");
                    CameraState.Play("CloseUpArc");
                    CloseUpArcShotCamera.LookAt = PlayerUnits[j].transform;
                    yield return StartCoroutine(PlayerUnits[j].Act());

                    PlayerUnits[j].ATBGauge = 0;
                    PanShotCamera.LookAt = PlayerUnits[j].transform;
                    CameraState.Play("Pan");
                }

                if(!PlayersActed.Contains(j))
                {
                    PlayersActed.Enqueue(j);
                }
                Debug.Log(PlayersActed.Count);
            }

            if(EnemiesActed.Count == EnemyManager.Instance.EnemyPartySize && PlayersActed.Count == PartyManager.Instance.PartySize)
            {
                Turn++;
                EnemiesActed.Clear();
                PlayersActed.Clear();
            }

            CameraState.Play("Arc");
            yield return null;
        }
        yield return null;
    }

    IEnumerator UseSkill(SkillID Skill, int EntityID, bool IsPlayer, int TargetID, bool TargetIsPlayer)
    {
        Skills SkillInfo = SkillManager.Instance.SkillData[(int)Skill];

        int Hits = UnityEngine.Random.Range(SkillInfo.MinHits, SkillInfo.MaxHits);
        int Power = SkillInfo.Power;

        StartCoroutine(BattleAnnouncer.Instance.PrintText(SkillInfo.Name, IsPlayer, false));
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < Hits; i++)
        {
            switch (SkillInfo.Effect)
            {
                case SkillEffects.Damage:
                    double UATK, TDEF, ULV, TLV;

                    if (IsPlayer)
                    {
                        if(SkillInfo.isMagical)
                        {
                            UATK = (double)PlayerUnits[EntityID].CurrentAttack;
                        }
                        else
                        {
                            UATK = (double)PlayerUnits[EntityID].CurrentMagic;
                        }

                        ULV = (double)PlayerUnits[EntityID].Level;
                    }
                    else
                    {
                        if(SkillInfo.isMagical)
                        {
                            UATK = (double)EnemyUnits[EntityID].CurrentAttack;
                        }
                        else
                        {
                            UATK = (double)EnemyUnits[EntityID].CurrentMagic;
                        }

                        ULV = (double)EnemyUnits[EntityID].Level;
                    }

                    if(TargetIsPlayer)
                    {
                        if(SkillInfo.isMagical)
                        {
                            TDEF = (double)PlayerUnits[EntityID].CurrentResistance;
                        }
                        else
                        {
                            TDEF = (double)PlayerUnits[EntityID].CurrentDefense;
                        }

                        TLV = (double)PlayerUnits[EntityID].Level;
                    }
                    else
                    {
                        if(SkillInfo.isMagical)
                        {
                            TDEF = (double)PlayerUnits[EntityID].CurrentResistance;
                        }
                        else
                        {
                            TDEF = (double)PlayerUnits[EntityID].CurrentDefense;
                        }

                        TLV = (double)PlayerUnits[EntityID].Level;
                    }

                    int BaseDamage = (int)Math.Round(Math.Sqrt(UATK * Power * 5) / Math.Sqrt(UATK/TDEF) * (ULV/TLV)) + 1;
                    Debug.Log(BaseDamage);
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.Heal:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.ReduceHPTo1:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.ExactDamage:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.ExactHeal:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.DrainHP:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.DrainEP:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.EscapeBattle:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.HealAllHP:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.HealAllHPAndEP:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.HealAllEP:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
                case SkillEffects.WeaponAttack:
                    yield return StartCoroutine(BattleAnnouncer.Instance.PrintText("This Skill Effect is unimplemented.", false, true));
                break;
            }
        }

        yield return null;
    }
}
