using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class MenuCursor : MonoBehaviour
{
    [SerializeField] float cursorSpeed = 0.5f;
    [SerializeField] Transform leftCursorTransform;
    [SerializeField] Transform rightCursorTransform;

    IMenuItem selectedItem;

    internal void AddIMenuItem(MainMenuItem mainMenuItem)
    {
        selectedItem = mainMenuItem;
    }

    internal void RemoveIMenuItem()
    {
        selectedItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 MirroredInput = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        leftCursorTransform.position += (Vector3) MirroredInput * cursorSpeed * Time.deltaTime;
        rightCursorTransform.position += (Vector3) input * cursorSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && selectedItem != null)
        {
            selectedItem.OnClick();
        }
    }
}
