using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class MainMenuItem : MonoBehaviour, IMenuItem
{
    [SerializeField] Color highlightColorRight;
    [SerializeField] TMP_Text[] texts;

    public UnityEvent onClicked;
    public UnityEvent onStay;

    Color[] initialTextColor = new Color[2];

    MenuCursor cursor;
    private void Start()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            initialTextColor[i] = texts[i].color;
        }

        cursor = FindObjectOfType<MenuCursor>();
    }
    public void OnClick()
    {
        onClicked.Invoke();
    }

    public void OnHold()
    {
        onStay.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (cursor != null)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                TMP_Text text = texts[i];
                text.color = highlightColorRight;
            }
            cursor.AddIMenuItem(this);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (cursor != null)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                TMP_Text text = texts[i];
                text.color = initialTextColor[i];
            }
            cursor.RemoveIMenuItem();
        }

    }

}

public interface IMenuItem
{
    void OnClick();
    void OnHold();
}
