using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 17f;
    public float RunningSpeed = 0f;
	
	public bool CanMove;

    public Rigidbody rb;
    public Animator animator;
    
    public bool IsMoving;
	public bool ConfirmButton;
	public bool RunningButton;

    public float ActualMoveSpeed;

    public Vector3 Movement;
    public Vector3 FacingDirection;

    public GameObject SweatObject;

	private static PlayerMovement _instance;

	public int StepNumber = 0;
	AudioSource FootStepAudio;
	public AudioClip[] Step1;
	public AudioClip[] Step2;

	public Animator Animator
	{
		get
		{
			return animator;
		}
	}

	private void Awake()
	{
		if (_instance != null)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
			return;
		}
		_instance = this;
	}

	public static PlayerMovement Instance
	{
		get
		{
			return _instance;
		}
	}

	void Start()
	{
		//FootStepAudio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {

        //Get Input
        if(CanMove == true)
        {
        	Movement.x = Input.GetAxisRaw("Horizontal");
        	Movement.z = Input.GetAxisRaw("Vertical");
        	RunningButton = Input.GetButton("Cancel");
			ConfirmButton = Input.GetButtonUp("Confirm");

       	 	Movement = Movement.normalized;

       		if (Movement.x != 0 || Movement.z != 0)
        	{
        		IsMoving = true;
        		FacingDirection = Movement;
        	}
        	else
        	{
        		IsMoving = false;
        	}

        	if (RunningButton)
        	{
        		ActualMoveSpeed = MoveSpeed * RunningSpeed;
        	}
        	else
        	{
        		ActualMoveSpeed = MoveSpeed;
        	}

        	animator.SetFloat("Horizontal", FacingDirection.x);
        	animator.SetFloat("Vertical", FacingDirection.y);
        	animator.SetFloat("Speed", Movement.sqrMagnitude);

        	animator.SetBool("IsMoving", IsMoving);
            animator.SetBool("IsRunning", RunningButton);


        }
    }

    void FixedUpdate()
    {
        //Move Sprite
        if(CanMove == true)
        {
        	rb.MovePosition(rb.position + Movement * ActualMoveSpeed * Time.fixedDeltaTime);
        }
    }

	public void FootStep()
	{
		if(StepNumber == 0)
		{
			FootStepAudio.Stop();
			FootStepAudio.clip = Step1[0];
			FootStepAudio.Play();
			StepNumber++;
		}
		else
		{
			FootStepAudio.Stop();
			FootStepAudio.clip = Step2[0];
			FootStepAudio.Play();
			StepNumber = 0;
		}
	}

}
