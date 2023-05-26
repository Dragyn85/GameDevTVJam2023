using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class MainMenuItem : MonoBehaviour, IMenuItem
{
    [SerializeField] Color selectedColor;
    [SerializeField] TMP_Text[] texts;

    public UnityEvent onClicked;
    public UnityEvent onStay;

    Color initialTextColor;
    MenuCursor cursor;
    private void Start()
    {
        initialTextColor = texts[0].color;
        cursor = FindObjectOfType<MenuCursor>();
    }
    public void OnClick()
    {
        onClicked.Invoke();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (cursor != null)
        {
            foreach (var text in texts)
            {
                text.color = selectedColor;
            }
            cursor.AddIMenuItem(this);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (cursor != null)
        {
            foreach(var text in texts)
            {
                onStay?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cursor != null)
        {
            foreach (var text in texts)
            {
                text.color = initialTextColor;
            }
            cursor.RemoveIMenuItem();
        }

    }

}

public interface IMenuItem
{
    void OnClick();
}
