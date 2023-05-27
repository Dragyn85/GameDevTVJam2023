/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__2020.3.36f1/Editor/Data/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs
*/
using UnityEngine;

/// <summary>
/// Logic to Open a Closed Door, when colliding with a Switch.
/// </summary>
public class DoorSwitchBehavior : MonoBehaviour
{

    #region Attributes

    /// <summary>
    /// Impact on game
    /// </summary>
    public int scoreAmount = 10;
    public float timeAmount = 10.0f;


    #region Door Animation

    [Tooltip("Reference to the door object that will be opened")]
    [SerializeField]
    private GameObject _door;

    [Tooltip("Door's Animator to trigger:")]
    //[SerializeField]
    private Animator _doorAnimator;
    
    /// <summary>
    /// Animation Hash Name:  OpenDoor
    /// </summary>
    private static readonly int _OpenDoor = Animator.StringToHash("DooOpenToTheSide");
    
    #endregion Door Animation

    #region Switch Sound
    
    /// <summary>
    /// Audio to Play
    /// </summary>
    [Tooltip("Audio (.wav) to Play.")]
    public AudioClip switchSound;

    /// <summary>
    /// Sound Source Component 1, for the Switch Sound: It must be attached to this GameObject
    /// </summary>
    private AudioSource _audioSource1ForSwitchSound;
    
    // Random Pitch values:
    //
    [Tooltip("Minimum value for random generation of Pitch.")]
    public float minPitch = 0.8f;
    
    [Tooltip("Maximum value for random generation of Pitch.")]
    public float maxPitch = 1.2f;

    #endregion Switch Sound
    
    #region Door Opening Sound
    
    /// <summary>
    /// Audio to Play
    /// </summary>
    [Tooltip("Audio (.wav) to Play.")]
    public AudioClip doorOpeningSound;
    
    /// <summary>
    /// Sound Source Component 2, for the Door Sound: It must be attached to this GameObject
    /// </summary>
    private AudioSource _audioSource2ForDoorOpeningSound;

    /// <summary>
    /// Reference to the Object in Door GameObject, that can call these SOund Functions for the Door Opening Animation.
    /// </summary>
    private DoorAnimationAndSoundsBehavior _doorAnimationAndSoundsBehavior;

    #endregion Door Opening Sound
    
    #region Optimization

    /// <summary>
    /// Cache of the GameObject to which this script is attached to...
    /// </summary>
    private GameObject _thisGameObject;

    /// <summary>
    /// Cache of this GameObject's Material...
    /// </summary>
    private Material _material;


    #endregion Optimization

    #endregion Attributes


    #region Unity Methods

    /// <summary>
    /// Awake is called before the Start calls round
    /// </summary>
    void Awake()
    {
	    #region Optimization, cache, etc

	    // Cache of the GameObject to which this script is attached to...
	    //
	    _thisGameObject = this.gameObject;

	    #endregion Optimization, cache, etc
	    
	    #region FOR:  1- Animations of the Switch

	    // FOR:  1- Animations of the Switch
	    //  FOR:  1.1- Turn off this GameObject's Emission of the material
	    //  Access the Renderer:  The Renderer provides access to the material used by the GameObject.
	    //
	    if ( _thisGameObject.TryGetComponent(out Renderer renderer) )
	    {
		    // Get the Material
		    //
		    _material = renderer.material;

	    }//End if ( _thisGameObject.TryGetComponent(
	    
	    // FOR Door Animation: 2- Get the Animator component from the door GameObject
	    //
	    _doorAnimator = _door.GetComponent<Animator>();
	    
	    #endregion FOR:  1- Animations of the Switch

	    #region FOR:  3.1- and 3.2- Playing Sounds:  Switch and Door Opening
		
	    // Get or add AudioSource components for SWITCH sound and DOOR sound
		//
	    _audioSource1ForSwitchSound = GetOrCreateAudioSource(switchSound, false);
	    _audioSource2ForDoorOpeningSound = GetOrCreateAudioSource(doorOpeningSound, true);
	    
	    // Door Sound related to the Animation
	    //
	    _doorAnimationAndSoundsBehavior = GetOrAddCreateDoorAnimationANdSoundsScriptInDoorGameObject();

	    #endregion FOR:  3.1- and 3.2- Playing Sounds:  Switch and Door Opening


    }//End Awake


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>



    /// <summary>
    /// Update is called once per frame
    /// </summary>


    /// <summary>
	/// When Player collides with this GameObject...
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other)
	{
		// Exit if there is a game manager and the game is over
		//
		// Todo Ale:  Add and Program the GameManager.cs  here
		//
		Debug.LogWarning($"Todo:  Add and Program the GameManager.cs  here.\n\n * door.SetActive(false); // Deactivate the door object.");
		//
		// if (GameManager.gm)
		// {
		// 	if (GameManager.gm.gameIsOver)
		// 		return;
		// }

		if (other.CompareTag("Player"))
		{

			// Todo Ale
			// Logic when pressing the Switch
			//
			Debug.LogWarning($"Todo Ale:  Logic when pressing the Switch.");
			
			
			// Logic + Animations + Sound... when pressing the Switch:
			//
			ActivateDoorSwitch();
			
			
			// if (explosionPrefab)
			// {
			// 	// Instantiate an explosion effect at the gameObjects position and rotation
			// 	//Instantiate (explosionPrefab, transform.position, transform.rotation);
			// }
	
			// if game manager exists, make adjustments based on target properties
			// if (GameManager.gm)
			// {
			// 	GameManager.gm.targetHit (scoreAmount, timeAmount);
			// }
			//
			// Todo Ale:  Add and Program the GameManager.cs  here
			// door... something
			//
			Debug.LogWarning($"Todo:  Add and Program the GameManager.cs  here.\n\n * If game manager exists, make adjustments based on target properties.");

		}//End if (other.CompareTag("Player"))
	}//End OnTriggerEnter

    #endregion Unity Methods
    

    #region My Custom Methods
    
    /// <summary>
    /// Logic to activate the Door Switch
    /// </summary>
    private void ActivateDoorSwitch()
    {
	    // 1- Animations of the Switch
		
	    //  1.1- Turn off this GameObject's Emission of the material
	    //  Access the Renderer:  The Renderer provides access to the material used by the GameObject.
	    //
	    if ( _material != null )
	    {
		    // Turn off emission
		    _material.DisableKeyword("_EMISSION");
            
		    // Update the material on the GameObject
		    //renderer.material = material;
		    
		    // Change Albedo color to black
		    _material.SetColor("_BaseColor", Color.black);
            
		    // Update the material on the GameObject
		    //renderer.material = material;

	    }//End if ( _thisGameObject.TryGetComponent(
	    
	    //  1.2- Turn off this CHILD's GameObject's Point Light, if there is one...
	    //
	    // Turn off Point Light on child GameObject
	    //
	    DisablePointLight(_thisGameObject);

	    
	    // 2- Animations of the Door
		
	    //  2.1- Turn ON: Animation of the Door Moving  (NOTE: add a SOUND event (looping) in the Animator to the Animations of the door, until it ends moving)
	    //
	    if (_doorAnimator != null)
	    {
		    // Play the door moving animation
		    //
		    _doorAnimator.SetTrigger(_OpenDoor);
	    }
	    //  2.2- When it touches another INVISIBLE TRIGGER, or (plan b: when it ends its movement in the Animation, can't be LOOPING Animation):   Stop  all this Animation
	    
	    
	    // 3- Sounds involved
		
	    //  3.1- Switch
	    //
	    // Play the switch sound
	    //
	    PlayShortSoundWithRandomPitch();

	    //  3.2- Door
	    //  See connection with another Script (the attribute / Field here is):   private DoorAnimationAndSoundsBehavior _doorAnimationAndSoundsBehavior;
		
	    // 4- Miscellaneous Logic Involved
		
	    //  4.1- Movement of Cinemachine Camera to a GameObject's position (for some seconds):  to show the action...
	    // NOT NECESSARY
		
	    //  4.2- Movement of Cinemachine Camera to the Original GameObject's position to return to its normal place or location...
	    // NOT NECESSARY
		
    }// End ActivateTheDoorSwitch


    /// <summary>
    /// Turns off the Light Component in a child gameobject
    /// </summary>
    /// <param name="parentObject"></param>
    private void DisablePointLight(GameObject parentObject)
    {
	    // Check if the parentObject has any child GameObjects
	    //
	    if (parentObject.transform.childCount > 0)
	    {
		    // Get the first child GameObject
		    //
		    GameObject childObject = parentObject.transform.GetChild(0).gameObject;
            
		    // Check if the childObject has a Point Light component
		    //
		    Light pointLight = childObject.GetComponent<Light>();
		    
		    if (pointLight != null)
		    {
			    // Disable the Point Light component
			    //
			    pointLight.enabled = false;
		    }
	    }//End if (parentObject.transform.childCount > 0)
    }//End DisablePointLight
    
    
    /// <summary>
    /// Play a sound with an Random Pitch, short-lived,
    /// </summary>
    private void PlayShortSoundWithRandomPitch()
    {
	    // Set a random pitch for the sound
	    //
	    float randomPitch = Random.Range(minPitch, maxPitch);

	    // Configure the AudioSource settings
	    //
	    // It is done in Awake time:  _audioSource.clip = switchSound;
	    _audioSource1ForSwitchSound.pitch = randomPitch;

	    // Play the sound
	    //
	    _audioSource1ForSwitchSound.Play();
    }

    /// <summary>
    /// To be played by an ANIMATION EVENT (see the Animation Clip for the Door Prefab)
    /// </summary>
    public void PlayDoorSound()
    {
	    // Play the door sound with a random pitch
	    //
	    float randomPitch = Random.Range(minPitch, maxPitch);
	    
	    _audioSource2ForDoorOpeningSound.pitch = randomPitch;
	    _audioSource2ForDoorOpeningSound.Play();
    }

    /// <summary>
    /// Stops an ANIMATION EVENT of Playing a Sound (see the Animation Clip for the Door Prefab)
    /// </summary>
    public void StopDoorSound()
    {
	    // Stop playing the door sound
	    //
	    _audioSource2ForDoorOpeningSound.Stop();
    }

	/// <summary>
	/// Initialization of 'AudioSource's in this script,
	/// </summary>
	/// <param name="audioClip"></param>
	/// <param name="secondAudioSource"></param>
	/// <returns></returns>
    private AudioSource GetOrCreateAudioSource(AudioClip audioClip, bool secondAudioSource)
    {
	    // List of A.S. that's already present:
	    //
	    AudioSource[] audioSourceList = GetComponents<AudioSource>();

	    // A.S. to Add and: Return
	    //
	    AudioSource audioSource = GetComponent<AudioSource>();

	    if (secondAudioSource)
	    {
		    if ((audioSourceList == null) || (audioSourceList.Length <= 1) || (audioSourceList[0] == null) || (audioSourceList[1] == null))
		    {
			    // Add an A.S.
			    //
			    audioSource = gameObject.AddComponent<AudioSource>();
		    }
		    // else
		    // {
		    // }
	    }
	    else
	    {
		    // This is the First AudioSource:
		    //
		    if (audioSource == null)
		    {
			    audioSource = gameObject.AddComponent<AudioSource>();
		    }

	    }//ENd else of if (secondAudioSource)

	            
	    if (audioClip != null)
	    {
		    audioSource.clip = audioClip;
	    }
	    // Config the AS:
	    //
	    audioSource.playOnAwake = false;

	    return audioSource;

    }//End GetOrCreateAudioSource
    
    private DoorAnimationAndSoundsBehavior GetOrAddCreateDoorAnimationANdSoundsScriptInDoorGameObject()
    {
	    // Check to see if there is a reference to the Script, first:
	    //
	    DoorAnimationAndSoundsBehavior doorAnimationAndSoundsBehavior = _door.GetComponent<DoorAnimationAndSoundsBehavior>();
	    
	    if (doorAnimationAndSoundsBehavior == null)
	    {
		    doorAnimationAndSoundsBehavior = _door.AddComponent<DoorAnimationAndSoundsBehavior>();
	    }
	    
	    // Also Add this script to the 'DoorAnimationAndSoundsBehavior'
	    //
	    doorAnimationAndSoundsBehavior.DoorSwitchBehaviorScript = this;

	    return doorAnimationAndSoundsBehavior;

    }//End GetOrAddCreateDoorAnimationANdSoundsScriptInDoorGameObject

    #endregion My Custom Methods
}
