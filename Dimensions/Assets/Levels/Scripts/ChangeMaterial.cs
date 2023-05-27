using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    #region Attributes
    
    private Material _initialDefaultMaterial;

    [Tooltip("New Material to Change To...")]
    public Material newMaterial;

    private MeshRenderer _meshRenderer;
    
    #endregion Attributes
    
    
    void Awake()
    {
        // Get a reference to the MeshRenderer component
        _meshRenderer = GetComponent<MeshRenderer>();
        //
        // Get the Default initial material:
        //
        if (_meshRenderer == null)
        {
            // Detach the script from the GameObject
            Destroy(GetComponent<ChangeMaterial>());
        }
        else
        {
            // Everything is fine, so do your work:
            //
            _initialDefaultMaterial = _meshRenderer.sharedMaterial;

        }//End else

    }// End Awake
    
    
    // Its was just to test the functionality. DO NOT USE IT
    // public void Update()
    // {
    //     // // Check for a specific condition to change the material
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         // Assign the new material to the MeshRenderer
    //         _meshRenderer.sharedMaterial = newMaterial;
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.LeftControl))
    //     {
    //         // Assign it its Original Default Material:
    //         //
    //         _meshRenderer.sharedMaterial = _initialDefaultMaterial;
    //     }
    //
    // }//End Update
    

    public void ChangeSharedMaterialTo(bool toSecondaryMaterial)
    {
        // Check for a specific condition to change the Material
        //
        if (toSecondaryMaterial)
        {
            // Assign the new material to the MeshRenderer
            //
            _meshRenderer.sharedMaterial = newMaterial;
        }
        else
        {
            // Assign it its Original Default Material:
            //
            _meshRenderer.sharedMaterial = _initialDefaultMaterial;
        }
    }//End ChangeSharedMaterialTo

}
