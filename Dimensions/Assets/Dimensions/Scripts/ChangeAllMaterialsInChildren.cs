using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAllMaterialsInChildren : MonoBehaviour
{
    [SerializeField] Material newMaterial;

    [ContextMenu("Switch material")]
    public void ChangeMaterial()
    {
        foreach(var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = newMaterial;
        }
    }
}
