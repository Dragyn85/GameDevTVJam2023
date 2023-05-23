using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 10f;
    [SerializeField] private float minScale = 1;
    [SerializeField] private float maxScale = 10f;
    [SerializeField] private float startScale = 1f;

    private float currentScale;

    
    private void Start()
    {
        transform.localScale = Vector3.one * startScale;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            float newScale = currentScale + scaleSpeed * Time.deltaTime;
            SetScale(newScale);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            float newScale = currentScale - scaleSpeed * Time.deltaTime;
            SetScale(newScale);
        }
    }

    private void SetScale(float scale)
    {
        currentScale = Mathf.Clamp(scale, minScale, maxScale);
        transform.localScale = Vector3.one * currentScale;
    }
}
