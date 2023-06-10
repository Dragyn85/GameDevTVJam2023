using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ" <br />
/// With a blend tree to control the inputmagnitude and allow blending between animations. <br />
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovementInput : MonoBehaviour
{

    public float Velocity;
    [Space]
    
    [Tooltip("True: Means this Character will move with the Horizontal x-axis inverted: Left is Right, Right is Left.")]
    [SerializeField]
    private bool _invertHorizontalDirectionMovement = false;

	public float     InputX;
	public float     InputZ;
	public Vector3   desiredMoveDirection;
	public bool      blockRotationPlayer;
	public float     desiredRotationSpeed = 0.1f;
	public Animator  anim;
	public float     Speed;
	public float     allowPlayerRotation = 0.1f;
	public Camera    cam;
	public Rigidbody rigidbody;
	public bool      isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;
    
    private static readonly int _Blend = Animator.StringToHash("Blend");

    public float verticalVel;
    private Vector3 _moveVector;

    
    void Awake ()
    {
	    anim = this.GetComponent<Animator> ();
	    cam = Camera.main;
	    rigidbody = this.GetComponent<Rigidbody> ();
    }
    
    //// Use this for initialization
	// void Start ()
	// {
	// 	anim = this.GetComponent<Animator> ();
	// 	cam = Camera.main;
	// 	rigidbody = this.GetComponent<CharacterController> ();
	// }
	
	
	// Update is called once per frame
	void Update () 
	{
		InputMagnitude();

		/*			ToDo: implement
        isGrounded = rigidbody.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 1;
        }
		
        _moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        
        rigidbody.MovePosition(_moveVector);
        */

	}// End Update

    void PlayerMoveAndRotation()
    {
	    // Get input from Keyboard
	    //
		GetInputXZAndSetHorizontalDirectionOfMovement();

		// Original code:   var camera = Camera.main;
		var transform1 = cam.transform;
		var forward = transform1.forward;
		var right = transform1.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		// Here the Movement Direction is calculated:
		//
		desiredMoveDirection = forward * InputZ + right * InputX;

		var grounded = Physics.Raycast(transform.position, Vector3.down, 1);

		if (grounded)
		{
			var vel = desiredMoveDirection * Velocity;
			rigidbody.velocity = vel;
		}

		if (blockRotationPlayer == false)
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
			rigidbody.angularVelocity = Vector3.zero;
		}
	}

    private void GetInputXZAndSetHorizontalDirectionOfMovement()
    {
	    InputX = Input.GetAxis("Horizontal");
	    InputZ = Input.GetAxis("Vertical");

	    // Calculate the real Horizontal Direction:
	    //
	    if (_invertHorizontalDirectionMovement)
	    {
		    InputX *= -1.0f;
	    }
	    // else
	    // {
	    // 	
	    // }//End if (_invertHorizontalDirectionMovement)
    }

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        // Original:   var camera = Camera.main;
        var transform1 = cam.transform;
        var forward = transform1.forward;
        var right = transform1.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

	void InputMagnitude()
	{
		// Get input from Keyboard
		//
		GetInputXZAndSetHorizontalDirectionOfMovement();

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

		// Calculate the Input Magnitude
		//
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        // Physically move player
		//
		if (Speed > allowPlayerRotation)
		{
			anim.SetFloat (_Blend, Speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation ();
		}
		else if (Speed < allowPlayerRotation)
		{
			anim.SetFloat (_Blend, Speed, StopAnimTime, Time.deltaTime);
		}
	}
}
