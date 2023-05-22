using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        Close();
    }

    public void Open()
    {
        _canvas.enabled = true;
    }
    public void Close()
    {
        _canvas.enabled = false;
    }
}
