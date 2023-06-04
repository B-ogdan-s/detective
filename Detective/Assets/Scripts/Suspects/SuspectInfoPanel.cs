using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectInfoPanel : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public System.Action CloseAction;

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
        CloseAction?.Invoke();
    }
}
