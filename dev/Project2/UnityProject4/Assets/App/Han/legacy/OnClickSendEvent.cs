using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickSendEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string eventName;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Events.onButtonDown.OnNext(eventName);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Events.onButtonUp.OnNext(eventName);
    }
}
