using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timeline : MonoBehaviour
{
    public GameObject EntityPrefab;

    public TextMeshProUGUI TurnText;

    public GameObject[] PlayerEntity;
    public GameObject[] EnemyEntity;
    // Start is called before the first frame update
    void Start()
    {
        PlayerEntity = new GameObject[PartyManager.Instance.PartySize];
        EnemyEntity = new GameObject[EnemyManager.Instance.EnemyPartySize];
        
        for (int i = 0; i < PartyManager.Instance.PartySize; i++)
        {
            PlayerEntity[i] = Instantiate(EntityPrefab, Vector3.zero, Quaternion.identity, transform);
            PlayerEntity[i].transform.localPosition = new Vector3(160f,-16f);
        }

        for (int j = 0; j < EnemyManager.Instance.EnemyPartySize; j++)
        {
            EnemyEntity[j] = Instantiate(EntityPrefab, Vector3.zero, Quaternion.identity, transform);
            EnemyEntity[j].transform.localPosition = new Vector3(-160f,-16f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TurnText.text = BattleManager.Instance.Turn.ToString();
        for (int i = 0; i < PartyManager.Instance.PartySize; i++)
        {
            float XPos = Mathf.Lerp(-160f, 0f, (float)BattleManager.Instance.PlayerUnits[i].ATBGauge / 50000f);
            PlayerEntity[i].transform.localPosition = new Vector3(XPos,-16f);
        }

        for (int j = 0; j < EnemyManager.Instance.EnemyPartySize; j++)
        {
            float XPos = Mathf.Lerp(160f, 0f, (float)BattleManager.Instance.EnemyUnits[j].ATBGauge / 50000f);
            EnemyEntity[j].transform.localPosition = new Vector3(XPos,-16f);
        }
    }
}
