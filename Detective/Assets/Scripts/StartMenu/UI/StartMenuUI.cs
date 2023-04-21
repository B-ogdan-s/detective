using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas _startMenuCanvas;
    [SerializeField] private Canvas _lobbyCanvas;

    public void Awake()
    {
        _startMenuCanvas.enabled = true;
        _lobbyCanvas.enabled = false;
    }

    public void OpenLobby()
    {
        _startMenuCanvas.enabled = false;
        _lobbyCanvas.enabled = true;
    }

    public void CloseLobby()
    {
        _startMenuCanvas.enabled = true;
        _lobbyCanvas.enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
