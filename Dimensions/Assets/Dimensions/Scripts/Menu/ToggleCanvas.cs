using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    CanvasGroup cg;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            cg.alpha = cg.alpha == 0 ? 1 : 0;
            cg.interactable = !cg.interactable;
            cg.blocksRaycasts = !cg.blocksRaycasts;
        }
    }
}
