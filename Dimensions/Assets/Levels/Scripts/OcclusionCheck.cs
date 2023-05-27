/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__2020.3.36f1/Editor/Data/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs
*/
using UnityEngine;


public class OcclusionCheck : MonoBehaviour
{

    #region Attributes

    [Tooltip("The Camera that shoots to the Player")]
    //[SerializeField]
    private Transform _playerCamera;
    
    
    /// <summary>
    /// The last GameObject that was Occluding this GameObject.
    /// </summary>
    private bool _wasThereALastGameObjectThatWasOccluding = false;    
    
    /// <summary>
    /// The last GameObject that was Occluding this GameObject.
    /// </summary>
    private ChangeMaterial _lastGameObjectThatWasOccluding;

    /// <summary>
    /// The current (in this frame) GameObject that was Occluding this GameObject.
    /// </summary>
    private ChangeMaterial _currentGameObjectThatWasOccluding;

    #endregion Attributes


    #region Unity Methods

    /// <summary>
    /// Awake is called before the Start calls round
    /// </summary>
    void Awake()
    {
        // Assuming your camera is tagged as "MainCamera"
        //
        if (Camera.main != null)
        {
            _playerCamera = Camera.main.transform;
        }
    }


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>



    /// <summary>
    /// Update is called once per frame
    /// TODO Alejandro:  (Performance Optimization): Move this Execution of Update to a Coroutine, that performs it every 7 frames of a little more...
    /// </summary>
    private void Update()
    {
        int maxHits = 4;
        RaycastHit[] hits = new RaycastHit[maxHits];

        Vector3 rayOrigin = transform.position;
        var position = _playerCamera.position;
        Vector3 rayDirection = position - rayOrigin;
        float rayDistance = Vector3.Distance(rayOrigin, position);

        int hitCount = Physics.RaycastNonAlloc(rayOrigin, rayDirection, hits, rayDistance);
        
        // Continue with the   RaycastNonAlloc  logic:
        //
        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].transform.CompareTag($"Wall") /*|| hits[i].transform.CompareTag($"Obstacle")*/)
                {
                    // Perform dithering effect
                    //
                    if (hits[i].collider.gameObject.TryGetComponent(out ChangeMaterial changeMaterial))
                    {
                        
                        // Save the ChangeMaterial.cs script of the GameObject.. so we can reestablish the Default Material in a latter Update, some frames after this one:
                        //
                        _currentGameObjectThatWasOccluding = changeMaterial;


                        // Reestablish the original material of the previous GameObject that was Ocluding this GameObject
                        //
                        if ( _wasThereALastGameObjectThatWasOccluding )
                        {
                            
                            // If the GameObjects are DIFFERENT:  Do it!
                            //
                            if (!_currentGameObjectThatWasOccluding.Equals(_lastGameObjectThatWasOccluding))
                            {
                            
                                // Change the Material to the Secondary one:
                                //
                                changeMaterial.ChangeSharedMaterialTo(true);
                                
                                // ...And reestablish the other material:
                                //
                                // Reestablish the original material of the previous GameObject that was Ocluding this GameObject
                                //
                                _lastGameObjectThatWasOccluding.ChangeSharedMaterialTo(false);

                                
                            }// End if (!_currentGameObjectThatWasOccluding.Equals(_lastGameObjectThatWasOccluding))
                            // else
                            // {
                            // }

                        }//End if ( _wasThereALastGameObjectThatWasOccluding )
                        else
                        {
                            // This is the FIRST TIME we Occlude any GameObject:
                            
                            // Change the Material to the Secondary one:
                            //
                            changeMaterial.ChangeSharedMaterialTo(true);

                        }//End else of if ( _wasThereALastGameObjectThatWasOccluding )
                        
                        
                        // Update the last GameObject:
                        //
                        _wasThereALastGameObjectThatWasOccluding = true;
                        //
                        _lastGameObjectThatWasOccluding = _currentGameObjectThatWasOccluding;

                    }//End if (hits[i].collider.gameObject.TryGetComponent(out ChangeMaterial changeMaterial))

                }//End if (hits[i].transform.CompareTag($"Wall")
                        
                // End the For loop
                //
                return;

            }//End for
            
        }//End if hitCount > 0...
        else
        {
            // Reestablish the original material of the previous GameObject that was Ocluding this GameObject
            //
            if ( _wasThereALastGameObjectThatWasOccluding )
            {

                // Reestablish the original material of the previous GameObject that was Ocluding this GameObject
                //
                _lastGameObjectThatWasOccluding.ChangeSharedMaterialTo(false);

                // Update the last GameObject:
                //
                _wasThereALastGameObjectThatWasOccluding = false;
                //
                //_lastGameObjectThatWasOccluding = nothing, not important

            }//End if ( _wasThereALastGameObjectThatWasOccluding )

        }// End else

    }//End Update

    #endregion Unity Methods
    

    #region My Custom Methods





    #endregion My Custom Methods

}
