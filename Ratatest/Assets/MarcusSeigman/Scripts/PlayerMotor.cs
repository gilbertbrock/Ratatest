using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private bool isRunning = false;
    //Lane Distance
    private const float Lane_Distance = 3.0f;
    private const float Turn_Speed = 0.05f;

    //Movement
    private CharacterController controller;
    public float jumpForce = 4.0f;
    public float gravity = 12.0f;
    private float verticalVelocity;

    //SpeedMoifier;
    private float originalSpeed = 7.0f;
    public float speed;
    public float maxSpeed = 21.0f;
    private float speedIncreaseLastTick;
    public float speedIncreaseTime;
    public float speedIncreaseAmount;

    private int desiredLane = 1; // 0 left, 1 middle, 2 right.
    
    //Size Change
    public Vector3 smallSize;
    public Vector3 largeSize;


    //Sound Clips
    public AudioClip Squeak1;
    public AudioClip Squeak2;
    public AudioClip Death;
    private AudioSource PlayerAudio;


    //Animator
    private Animator RatAnim;


    private void Awake()
    {
        RatAnim = GetComponent<Animator>();
      PlayerAudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {

        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {

        controller.skinWidth = .01f;
        if (transform.position.y < .0099f)
            transform.position = new Vector3(transform.position.x, transform.position.y + (0.0099f - transform.position.y), transform.position.z);

        if (!isRunning)
            return;
        
        if( Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            if (speed < maxSpeed)
            {
                speed += speedIncreaseAmount;
            }
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }

        
        //Gather the inputs for lanes
        // move left
        /*if(Input.GetKeyDown(KeyCode.A))
            MoveLane(false);
        //move right.
        if (Input.GetKeyDown(KeyCode.D))
            MoveLane(true);*/

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (desiredLane == 0)
            targetPosition += Vector3.left * Lane_Distance;
        else if(desiredLane == 2)
            targetPosition += Vector3.right * Lane_Distance;

        //Calculate move delta
        Vector3 moveVector = Vector3.zero;
        
        //Moves the character forward
        //moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        //Caculate Y gravity
        if(IsGrounded())//if grounded
        {
            verticalVelocity = -0.1f;

            if(Input.GetKeyDown(KeyCode.Space) || MobileInput.Instance.SwipeUp)
            {

                //jump
                PlayerAudio.PlayOneShot(Squeak1);
                RatAnim.SetTrigger("RatJump");
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            //fast falling mechanic
            if(Input.GetKeyDown(KeyCode.S) || MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }
        }


        moveVector.y = verticalVelocity;

        if (isRunning)
        {
            RatAnim.SetTrigger("RatBeginPlay");
            moveVector.z = speed;
        }

        //Move Character
        controller.Move(moveVector * Time.deltaTime);


        //rotate the character to where it is going.
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, Turn_Speed);
        }

        //Size Changing controls
        if(Input.GetKeyDown(KeyCode.W) || MobileInput.Instance.SwipeLeft || MobileInput.Instance.SwipeRight)
        {

            if (transform.localScale == smallSize)
            {
                PlayerAudio.PlayOneShot(Squeak2);
                transform.localScale = largeSize;
                jumpForce = 7.2f;
            }
            else if (transform.localScale == largeSize)
            {
                PlayerAudio.PlayOneShot(Squeak2);
                transform.localScale = smallSize;
                jumpForce = 5.2f;
            }
        }
    }

   /* private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }*/

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3( 
                controller.bounds.center.x,
                (controller.bounds.center.y - controller.bounds.extents.y) + .1f,
                controller.bounds.center.z),
             Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.green, 1.0f);

        return (Physics.Raycast(groundRay, 0.12f));
        
    }

    private void Crash()
    {
        print("is dead");
        RatAnim.SetTrigger("RatDead");
        PlayerAudio.PlayOneShot(Death);
        isRunning = false;
        GameManager.Instance.OnDeath();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }

    public void StartRunning()
    {
        isRunning = true;
    }

}
