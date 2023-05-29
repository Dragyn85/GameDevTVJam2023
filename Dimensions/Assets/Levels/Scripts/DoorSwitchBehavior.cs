/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__2020.3.36f1/Editor/Data/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs
*/
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

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

	#region SoundSetting
	[SerializeField, Tooltip("Mixer group that will be added to the AudioSource")] AudioMixerGroup sfxMixerGroup;
    #endregion

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
    [Header("1- 'Good' Random Pitch values:  For any TRUE Switch")]
    
    [Tooltip("Minimum value for random generation of Pitch.")]
    public float minPitchTrueSwitch = 0.8f;
    
    [Tooltip("Maximum value for random generation of Pitch.")]
    public float maxPitchTrueSwitch = 1.2f;

    
    [Header("2- 'Wrong' Random Pitch values:  For any TRAP, FALSE or 'WRONG' Switch")]
    
    [Tooltip("Minimum value for random generation of Pitch.")]
    public float minWrongPitch = 0.07f;
    
    [Tooltip("Maximum value for random generation of Pitch.")]
    public float maxWrongPitch = 0.5f;

    [Tooltip("Maximum value for random generation of Pitch.")]
    [SerializeField]
    private float _fractionOfDurationForWrongSound = 2.0f;

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
    
    /// <summary>
    /// Cache of this GameObject's Material Property:  "_BaseColor"...
    /// </summary>
    private static readonly int _BaseColor = Shader.PropertyToID("_BaseColor");
    
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
	    if (_door != null)
	    {
		    _doorAnimator = _door.GetComponent<Animator>();

	    }//End if
	    
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
		if (GameManager.Gm)
		{
			if (GameManager.Gm.GameState == GameManager.GameStates.GameOver)
				return;
		}

		if (other.CompareTag("Player"))
		{

			// Logic when pressing the Switch
			// Logic + Animations + Sound... when pressing the Switch:
			//
			ActivateDoorSwitch();
			

			// if game manager exists, make adjustments based on target properties: Collect Points
			//
			if (GameManager.Gm)
			{
				GameManager.Gm.Collect(1);
			}

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
	    DeactivateMaterialEmissionOfSwitch();
	    
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
	    // Play the  SWITCH Sound
	    //
	    PlayShortSwitchSoundWithRandomPitch(_fractionOfDurationForWrongSound);

	    //  3.2- Door
	    //  The sound is played in the Door Animation.clip, by the Unity Animator of the Door Prefab, as an Unity "Animation Event".
	    //  See the connection from this script, to the other Script (the attribute / Field here is):   private DoorAnimationAndSoundsBehavior _doorAnimationAndSoundsBehavior;
		
	    // 4- Miscellaneous Logic Involved
		
	    //  4.1- Movement of Cinemachine Camera to a GameObject's position (for some seconds):  to show the action...
	    // NOT NECESSARY
		
	    //  4.2- Movement of Cinemachine Camera to the Original GameObject's position to return to its normal place or location...
	    // NOT NECESSARY
		
    }// End ActivateTheDoorSwitch

    
    #region LIGHT utils
    
    /// <summary>
    /// Disables the CheckBox:  Emission  on the Material via Code. 
    /// </summary>
    private void DeactivateMaterialEmissionOfSwitch()
    {
	    // 1- Animations of the Switch

	    //  1.1- Turn off this GameObject's Emission of the material
	    //  Access the Renderer:  The Renderer provides access to the material used by the GameObject.
	    //
	    if (_material != null)
	    {
		    // Turn off emission
		    _material.DisableKeyword("_EMISSION");

		    // Update the material on the GameObject
		    //renderer.material = material;

		    // Change Albedo color to black
		    _material.SetColor(_BaseColor, Color.black);

		    // Update the material on the GameObject
		    //renderer.material = material;

	    } //End if
    }// End DeactivateMaterialEmissionOfSwitch


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
    
    #endregion LIGHT utils
    
    #region SOUNDS utils
    
    /// <summary>
    /// Play a sound with an Random Pitch, short-lived (i.e.:  a special time duration for the Sound is set here, too...). <br /> <br />
    /// The pitch:  will be A TRUE / GOOD pitch: if the Field  _door  exists... <br /> <br />
    /// The pitch:  will be A FALSE / 'WRONG' pitch: if the Field  _door does NOT exist. <br /> <br />
    /// </summary>
    private void PlayShortSwitchSoundWithRandomPitch(float fractionOfDurationForSound)
    {
	    // Set a random pitch for the sound
	    //
	    float randomPitch;
	    
	    // Pitch:
	    //
	    if ( _door != null )
	    {
		    // Set a ( GOOD / RIGHT / TRUE ) random pitch for the sound
		    //
		    randomPitch = Random.Range(minPitchTrueSwitch, maxPitchTrueSwitch);
		    
		    // Set the Fraction of Duration of Sound to Play
		    //
		    fractionOfDurationForSound = 1.0f;
	    }
	    else
	    {
		    // Set a ( WRONG / TRAP / FALSE ) random pitch for the sound
		    //
		    randomPitch = Random.Range(minWrongPitch, maxWrongPitch);

		    // // Set the Fraction of Duration of Sound to Play
		    // //
		    // fractionOfDurationForWrongSound = fractionOfDurationForWrongSound; //nothing to do here...

	    }//End if Pitch...

	    // Configure the AudioSource settings
	    //
	    // It is done in Awake time:  _audioSource.clip = switchSound;
	    _audioSource1ForSwitchSound.pitch = randomPitch;

	    // Play the sound
	    //
	    // _audioSource1ForSwitchSound.Play();
	    //
	    PlaySoundOnlyForCertainDuration(_audioSource1ForSwitchSound, fractionOfDurationForSound);
    }

    /// <summary>
    /// Plays a sound that is already loaded in an 'AudioSource', for a certain time fractional 'duration' (for instance: to play a sound only b half of its duration, use fractionOfDuration = 2.0  ; so: time = (total / 2.0)  ).
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="fractionOfDuration"></param>
    public void PlaySoundOnlyForCertainDuration(AudioSource audioSource, float fractionOfDuration)
    {
	    // Validation
	    //
	    if (fractionOfDuration <= 0.01f)
	    {
		    //throw new ArgumentOutOfRangeException(nameof(duration));
		    
		    Debug.LogError($"Error: ArgumentOutOfRangeException(nameof(fractionOfDuration)... for the audioSource = {audioSource} passed to the function: 'PlaySoundOnlyForCertainDuration'  \n\n * Class / Script 'DoorSwitchBehavior.cs' {this.name}.\n\n Please Fix it in the Inspector.");
		    
		    return;
	    }
	    //
	    if (audioSource == null)
	    {
	        Debug.LogError($"Error: NullReferenceException... for the audioSource = {audioSource} passed to the function: 'PlaySoundOnlyForCertainDuration'  \n\n * Class / Script 'DoorSwitchBehavior.cs' {this.name}.\n\n Please Fix it in the Inspector.");
	        
	        return;
	    }

	    // // Load the audio clip into the AudioSource component
	    // audioSource.clip = YourAudioClip;

	    // Calculate the duration to play the sound (half of the clip's length)
	    float duration = audioSource.clip.length / fractionOfDuration;

	    // Get the scheduled end time
	    double scheduledEndTime = AudioSettings.dspTime + duration;

	    // Play the sound using PlayScheduled
	    audioSource.PlayScheduled(AudioSettings.dspTime);

	    // Set the scheduled end time
	    audioSource.SetScheduledEndTime(scheduledEndTime);
	    
	    // Assign the Sound to the AudioSource:
	    //
	    _audioSource1ForSwitchSound = audioSource;

	    // Stop the sound after the desired duration
	    //
	    Invoke(nameof(StopSound), duration);

    }// End PlaySoundOnlyForCertainDuration


    /// <summary>
    /// Stops any sound.
    /// </summary>
    public void StopSound()
    {
	    //audioSource.Stop();
	    
	    _audioSource1ForSwitchSound.Stop();
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
				audioSource.outputAudioMixerGroup = sfxMixerGroup;
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
                audioSource.outputAudioMixerGroup = sfxMixerGroup;
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

    #endregion SOUNDS utils
    
    
    #region SOUNDS for: Door Animation

    /// <summary>
    /// To be played by an ANIMATION EVENT (see the Animation Clip for the Door Prefab)
    /// </summary>
    public void PlayDoorSound()
    {
	    // Play the door sound with a random pitch
	    //
	    float randomPitch = Random.Range(minPitchTrueSwitch, maxPitchTrueSwitch);
	    
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
    /// Return a Script "DoorAnimationAndSoundsBehavior" to control the Play and Stop() functions of the Sounds of the Door Animation.<br /> <br />
    /// Note: It return  "null"  if the  "_door"  is null.
    /// </summary>
    /// <returns></returns>
    private DoorAnimationAndSoundsBehavior GetOrAddCreateDoorAnimationANdSoundsScriptInDoorGameObject()
    {
	    // Validation  Does _door  exist?
	    //
	    if ( _door == null )
	    {
		    return null;
	    }
	    
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
    
    #endregion  SOUNDS for: Door Animation
    
	#endregion My Custom Methods
}
