using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class ChangeScale : MonoBehaviour
{
    [SerializeField] private TouchInput _panel;
    [SerializeField] private CinemachineVirtualCamera _camera1;
    [SerializeField] private CinemachineVirtualCamera _camera2;
    [SerializeField] private float _time;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        _camera1.enabled = true;
        _camera2.enabled = false;
        _canvas.enabled = false;
        _panel.Scaler += OpenMap;
    }

    public void CloseMap()
    {
        StartCoroutine(CR_Close());
        _canvas.enabled = false;
    }

    private void OpenMap()
    {
        StartCoroutine(CR_Open());
        _panel.enabled = false;
    }

    private IEnumerator CR_Open()
    {
        _camera1.enabled = false;
        _camera2.enabled = true;
        yield return new WaitForSeconds(_time);
        _canvas.enabled = true;
    }
    
    private IEnumerator CR_Close()
    {
        _camera1.enabled = true;
        _camera2.enabled = false;
        yield return new WaitForSeconds(_time);
        _panel.enabled = true;
    }

    private void OnDestroy()
    {
        _panel.Scaler -= OpenMap;
    }

    private async void AsyncTest()
    {
        
    }
}
