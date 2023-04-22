using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas _startMenuCanvas;
    [SerializeField] private Canvas _lobbyCanvas; 
    [SerializeField] private Canvas _createRoom;
    [SerializeField] private Canvas _waitingMenu;

    [SerializeField] private PhotonMaster _photonMaster;

    private void Awake()
    {
        CloseCreateRoom();
        _startMenuCanvas.enabled = true;
        _lobbyCanvas.enabled = false;
        _waitingMenu.enabled = false;

        _photonMaster.JoinRoom += OpenWaitingMenu;
        _photonMaster.CloseWaitingMenu += CloseWaitingMenu;
    }

    public void OpenCreateRoom()
    {
        _createRoom.enabled = true;
    }
    public void CloseCreateRoom()
    {
        _createRoom.enabled = false;
    }

    private void OpenWaitingMenu()
    {
        _waitingMenu.enabled = true;
        _startMenuCanvas.enabled = false;
        _lobbyCanvas.enabled = false;
        CloseCreateRoom();
    }
    private void CloseWaitingMenu()
    {
        _waitingMenu.enabled = false;
        _startMenuCanvas.enabled = false;
        _lobbyCanvas.enabled = true;
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

    private void OnDestroy()
    {
        _photonMaster.JoinRoom -= OpenWaitingMenu;
        _photonMaster.CloseWaitingMenu -= CloseWaitingMenu;

    }
}
