using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPointer : Button
{
    public Action OnButtonDown;
    public bool isPressed = false;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        isPressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isPressed = false;
    }

    private void FixedUpdate()
    {
        if (isPressed)
        {
            OnButtonDown?.Invoke();
        }
    }
}