using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour,IPointerDownHandler
{
    public event Action Scaler;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Touch");
        Scaler?.Invoke();
    }
    
}
