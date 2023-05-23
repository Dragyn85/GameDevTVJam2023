using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 10f;
    [SerializeField] private float minScale = 0.01f;
    [SerializeField] private float maxScale = 100f;
    [SerializeField] private float startScale = 1f;

    private float currentScale;

    
    private void Start()
    {
        currentScale = transform.localScale.x;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            currentScale += scaleSpeed * Time.deltaTime;
            SetScale();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            currentScale -= scaleSpeed * Time.deltaTime;
            currentScale = Mathf.Clamp(currentScale, minScale, maxScale);
            transform.localScale = Vector3.one * currentScale;
        }
    }

    private void SetScale()
    {
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);
        transform.localScale = Vector3.one * currentScale;
    }
}
