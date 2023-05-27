using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 10f;
    [SerializeField] private float minScale = 0.01f;
    [SerializeField] private float maxScale = 100f;

    [Tooltip("Do you want to use 'Start Scale'? FALSE means you are only starting from the size the Transform (i.e.: the SCALE) Component's values are now.")]
    [SerializeField] private bool useStartScale = false;

    [SerializeField] private float startScale = 1f;

    private float _currentScale;

    
    private void Awake()
    {
        if (useStartScale)
        {
            // Use the value 'startScale'  in this script:
            //
            _currentScale = startScale;
        }
        else
        {
            // Use the Default Scale from this GameObjects' Transform:
            //
            _currentScale = transform.localScale.x;
        }
    }

    void Update()
    {
        // Change the Scale of the Character (GameObject) through code:
        //
        if (Input.GetKey(KeyCode.E))
        {
            _currentScale += scaleSpeed * Time.deltaTime;
            _currentScale =  Mathf.Clamp(_currentScale, minScale, maxScale);
            SetScale();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            _currentScale -= scaleSpeed * Time.deltaTime;
            _currentScale = Mathf.Clamp(_currentScale, minScale, maxScale);
            transform.localScale = Vector3.one * _currentScale;
        }
    }

    private void SetScale()
    {
        _currentScale = Mathf.Clamp(_currentScale, minScale, maxScale);
        transform.localScale = Vector3.one * _currentScale;
    }
}
