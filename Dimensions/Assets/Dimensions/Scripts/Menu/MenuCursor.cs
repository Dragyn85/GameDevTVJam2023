using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuCursor : MonoBehaviour
{
    [SerializeField] float cursorSpeed = 0.5f;
    [SerializeField] Transform leftCursorTransform;
    [SerializeField] Transform rightCursorTransform;
    [SerializeField] Transform maxBound;
    [SerializeField] Transform minBound;


    Animator leftAnimator;
    Animator rightAnimator;
    int blendHash = Animator.StringToHash("Blend");

    IMenuItem selectedItem;
    private void Awake()
    {
        leftAnimator = leftCursorTransform.GetComponentInChildren<Animator>();
        rightAnimator = rightCursorTransform.GetComponentInChildren<Animator>();
        leftAnimator.SetTrigger("Normal");
        rightAnimator.SetTrigger("Normal");
    }
    internal void AddIMenuItem(MainMenuItem mainMenuItem)
    {
        selectedItem = mainMenuItem;
        
    }

    internal void RemoveIMenuItem()
    {
        selectedItem = null;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 MirroredInput = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(input.magnitude > 0.1) 
        {
            leftAnimator.SetFloat(blendHash, 0.3f);
            rightAnimator.SetFloat(blendHash, 0.3f);
            rightCursorTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg+90);
            leftCursorTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(MirroredInput.y, MirroredInput.x) * Mathf.Rad2Deg+90);
        }
        else
        {
            leftAnimator.SetFloat(blendHash, 0f);
            rightAnimator.SetFloat(blendHash, 0f);
        }

        Vector3 lastLeftPos = leftCursorTransform.position;
        Vector3 lastRightPos = rightCursorTransform.position;

        leftCursorTransform.position += (Vector3)MirroredInput * cursorSpeed * Time.deltaTime;
        rightCursorTransform.position += (Vector3)input * cursorSpeed * Time.deltaTime;

        

        if (rightCursorTransform.position.x > maxBound.position.x ||
            rightCursorTransform.position.y > maxBound.position.y ||
            rightCursorTransform.position.x < minBound.position.x ||
            rightCursorTransform.position.y < minBound.position.y)
        {
            rightCursorTransform.position = lastRightPos;
            leftCursorTransform.position = lastLeftPos;
            rightAnimator.SetFloat(blendHash, 0f);
            leftAnimator.SetFloat(blendHash, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && selectedItem != null)
        {
            selectedItem.OnClick();
        }
    }
}
