using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BattleCommand : MonoBehaviour
{
    public Image[] WheelArrow;
    public Image WheelImage;
    public TextMeshProUGUI SelText;
    public string[] OptionText;
    public Color ArrowColor;

    public int Selection;
    public Vector2 Angle;

    static readonly Vector2 UpLeft = Vector2.up + Vector2.left;
    static readonly Vector2 UpRight = Vector2.up + Vector2.right;
    static readonly Vector2 DownLeft = Vector2.down + Vector2.left;
    static readonly Vector2 DownRight = Vector2.down + Vector2.right;

    public static BattleCommand Instance = null;

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
    
    public IEnumerator CommandWheel()
    {
        bool InMenu = true;
        InitArrow();
        yield return StartCoroutine(OpenCommandWheel());
        while(InMenu)
        {
            DrawArrow();
            yield return null;
        }

        yield return null;
    }

    public IEnumerator OpenCommandWheel()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        transform.DOScale(new Vector3(1, 1, 1), 0.4f);

        WheelImage.DOFade(1, 0.2f);
        SelText.DOFade(1, 0.2f);
        for (int i = 0; i < 8; i++)
        {
            WheelArrow[i].DOFade(1, 0.2f);
        }
        yield return new WaitForSeconds(0.4f);
    }

    public void InitArrow()
    {
        for (int i = 0; i < 8; i++)
        {
            WheelArrow[i].color = ArrowColor;
        }

        SelText.text = OptionText[8];
    }

    public void DrawArrow()
    {
        Angle.x = Input.GetAxisRaw("Horizontal");
        Angle.y = Input.GetAxisRaw("Vertical");

        switch (Angle)
        {
            default:
                Selection = -1;
            break;
            case Vector2 v when v.Equals(Vector2.up):
                Selection = 0;
            break;
            case Vector2 v when v.Equals(UpRight):
                Selection = 1;
            break;
            case Vector2 v when v.Equals(Vector2.right):
                Selection = 2;
            break;
            case Vector2 v when v.Equals(DownRight):
                Selection = 3;
            break;
            case Vector2 v when v.Equals(Vector2.down):
                Selection = 4;
            break;
            case Vector2 v when v.Equals(DownLeft):
                Selection = 5;
            break;
            case Vector2 v when v.Equals(Vector2.left):
                Selection = 6;
            break;
            case Vector2 v when v.Equals(UpLeft):
                Selection = 7;
            break;
        }

        for (int i = 0; i < 8; i++)
            {
                if(i == Selection)
                {
                    WheelArrow[i].color = Color.white;
                }
                else
                {
                    WheelArrow[i].color = ArrowColor;
                }
            }

        if(Selection != -1)
        {
            SelText.text = OptionText[Selection];
        }
        else
        {
            SelText.text = OptionText[8];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
