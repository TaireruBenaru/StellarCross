using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleAnnouncer : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Image Box;
    public Image Icon;

    public static BattleAnnouncer Instance = null;

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
    
    public IEnumerator OpenTextBox(bool IsPlayer, bool IsSystem)
    {
        Text.enabled = true;
        Box.enabled = true;
        Icon.enabled = true;

        if(IsPlayer)
        {
            transform.localPosition = new Vector3(25f, 75f);
        }
        else
        {
            transform.localPosition = new Vector3(-25f, 75f);
        }

        if(IsSystem)
        {
            Icon.enabled = false;
            transform.localPosition = new Vector3(0f, 50f);
        }

        transform.DOLocalMove(new Vector3(0f, 75f), 0.2f);
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator CloseTextBox(bool IsPlayer, bool IsSystem)
    {
        if(IsPlayer)
        {
            transform.DOLocalMove(new Vector3(25f, 75f), 0.2f);
        }
        else
        {
            transform.DOLocalMove(new Vector3(-25f, 75f), 0.2f);
        }

        if(IsSystem)
        {
            transform.DOLocalMove(new Vector3(0f, 50f), 0.2f);
        }
        yield return new WaitForSeconds(0.2f);

        Text.enabled = false;
        Box.enabled = false;
        Icon.enabled = false;

        yield return null;
    }

    public IEnumerator PrintText(string Dialogue, bool IsPlayer, bool IsSystem)
    {
        Text.text = Dialogue;

        yield return StartCoroutine(OpenTextBox(IsPlayer, IsSystem));
        if(IsSystem)
        {
            yield return new WaitForSeconds(1.6f);
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
        }
        yield return StartCoroutine(CloseTextBox(IsPlayer, IsSystem));
    }
}
