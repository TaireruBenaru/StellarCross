using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool UpButtonDown;
    public bool DownButtonDown;
    public bool LeftButtonDown;
    public bool RightButtonDown;

    public static InputManager Instance = null;

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
        UpButtonDown = Input.GetKey(KeyCode.UpArrow);
        DownButtonDown = Input.GetKey(KeyCode.DownArrow);
        LeftButtonDown = Input.GetKey(KeyCode.LeftArrow);
        RightButtonDown = Input.GetKey(KeyCode.RightArrow);
    }
}
