using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This Script is used to change the Scale of a GameObject through code, when pressing an Input Button on the Keyboard <br />
/// ( Input.GetKey(KeyCode.E) and Input.GetKey(KeyCode.Q) ).
/// </summary>
public class PlayerScale : MonoBehaviour
{

    [FormerlySerializedAs("scaleSpeed")]
    [SerializeField] private float _scaleSpeed = 10f;
    
    [FormerlySerializedAs("minScale")]
    [SerializeField] private float _minScale = 0.01f;
    
    [FormerlySerializedAs("maxScale")]
    [SerializeField] private float _maxScale = 100f;

    [FormerlySerializedAs("useStartScale")]
    [Tooltip("Do you want to use 'Start Scale'? FALSE means you are only starting from the size the Transform (i.e.: the SCALE) Component's values are now.")]
    [SerializeField]
    private bool _useStartScale = false;

    [FormerlySerializedAs("startScale")]
    [SerializeField] private float _startScale = 1f;

    private float _currentScale;

    
    private void Awake()
    {
        if (_useStartScale)
        {
            // Use the value 'startScale'  in this script:
            //
            _currentScale = _startScale;
        }
        else
        {
            // Use the Default Scale from this GameObjects' ('Scale' in the X-Axis) Transform:
            //
            _currentScale = transform.localScale.x;
        }
    }

    void Update()
    {
        // Change the Scale of the Character (GameObject) through code,
        //..when pressing an Input Button on the Keyboard:
        //
        if (Input.GetKey(KeyCode.E))
        {
            // Make it GROW (+) in Scale:
            //
            SetScale( 1 );
        }
        if (Input.GetKey(KeyCode.Q))
        {
            // Make it SHRINK (-) in Scale:
            //
            SetScale( -1 );
        }
    }

    /// <summary>
    /// Sets the value to "grow" or to "shrink" in this current time-frame, and applies it to: <br />
    /// ...the 'GameObject' this Script Component is attached to.
    /// </summary>
    /// <param name="direction"></param>
    private void SetScale(int direction)
    {
        // Get the value to "grow" or to "shrink" in this current time-frame:
        //
        _currentScale += _scaleSpeed * Time.deltaTime * direction;

        // Clamp the value to the Range:  [minScale, maxScale] 
        //
        _currentScale = Mathf.Clamp(_currentScale, _minScale, _maxScale);
        
        // Apply the change in 'Scale':
        //
        transform.localScale = Vector3.one * _currentScale;

    }// End SetScale
}
