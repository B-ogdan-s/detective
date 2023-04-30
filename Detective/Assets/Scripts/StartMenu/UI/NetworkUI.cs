using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Canvas _disconectCanvas;
    [SerializeField] private Canvas _waitingCanvas;

    public void OpenWaiting()
    {
        _disconectCanvas.enabled = false;
        _waitingCanvas.enabled = true;
    }

    public void CloseAll()
    {
        _waitingCanvas.enabled = false;
        _disconectCanvas.enabled = false;
    }

    public void Disconnected(DisconnectCause disconectMasege)
    {
        _disconectCanvas.enabled = true;
        _waitingCanvas.enabled = false;
    }
}
