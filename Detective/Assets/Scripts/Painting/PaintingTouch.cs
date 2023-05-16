using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaintingTouch : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        //Debug.Log(eventData.pointerCurrentRaycast.);
    }
}
