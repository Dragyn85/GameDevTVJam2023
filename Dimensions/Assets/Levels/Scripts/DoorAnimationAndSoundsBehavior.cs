/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__2020.3.36f1/Editor/Data/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs
*/
using UnityEngine;


public class DoorAnimationAndSoundsBehavior : MonoBehaviour
{

    #region Attributes

    [Tooltip("Reference to the Switch Script with the AudioSources and Audios for these Animations.")]
    [SerializeField]
    private DoorSwitchBehavior _doorSwitchBehaviorScript;

    public DoorSwitchBehavior DoorSwitchBehaviorScript
    {
        get => _doorSwitchBehaviorScript;
        set => _doorSwitchBehaviorScript = value;
    }

    #endregion Attributes


    #region Unity Methods

    /// <summary>
    /// Awake is called before the Start calls round
    /// </summary>
    private void Awake()
    {

        // if ( _doorSwitchBehaviorScript == null )
        // {
        //     Debug.LogError($"Error: NUllReferenceException... CLass / Script 'DoorSwitchBehavior.cs' {this.name}.\n\n Please Fix it in the Inspector.");
        // }
    }


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>



    /// <summary>
    /// Update is called once per frame
    /// </summary>


    #endregion Unity Methods
    

    #region My Custom Methods

    /// <summary>
    /// To be played by an ANIMATION EVENT (see the Animation Clip for the Door Prefab)
    /// </summary>
    public void PlayDoorSound()
    {
        _doorSwitchBehaviorScript.PlayDoorSound();
    }

    /// <summary>
    /// Stops an ANIMATION EVENT of Playing a Sound (see the Animation Clip for the Door Prefab)
    /// </summary>
    public void StopDoorSound()
    {
        _doorSwitchBehaviorScript.StopDoorSound();
    }
    

    #endregion My Custom Methods

}
