using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class PhotonMasterStartMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _regionID;
    [SerializeField] private ErrorPanel _errorPanel;

    private NetworkUI _networkUI;
    private bool _isStart = true;

    public Action<List<Photon.Realtime.RoomInfo>> UpdateRoomList;
    public Action JoinRoomAction;
    public Action CloseWaitingMenu;

    private void Awake()
    {
        _networkUI = GetComponent<NetworkUI>();
        Conect();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        _networkUI.OpenWaiting();
    }

    public void Conect()
    {
        _networkUI.OpenWaiting();

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_regionID);
    }

    public void CreateRoom(RoomInfo roomInfo)
    {
        _networkUI.OpenWaiting();
        Hashtable table = new Hashtable();

        table.Add("complexity", roomInfo.GameComplexity);
        table.Add("isPassword", roomInfo.isPassword);
        table.Add("password", roomInfo.RoomPasword);
        table.Add("minPeople", roomInfo.MinPeople);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = table;
        roomOptions.PublishUserId = true;
        roomOptions.MaxPlayers = roomInfo.MaxPeople;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "complexity", "minPeople", "password", "isPassword" };

        PhotonNetwork.CreateRoom(roomInfo.RoomName, roomOptions);
    }

    
    public void CloseRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region Callbacks

    public override void OnConnectedToMaster()
    {
        _networkUI.CloseAll();
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        if(_isStart)
        {
            _isStart = false;
            return;
        }

        _networkUI.Disconnected(cause);
    }
    public override void OnCreatedRoom()
    {
        JoinRoomAction?.Invoke();
        _networkUI.CloseAll();
        PlayerData.GameComplexity = (GameComplexity)PhotonNetwork.CurrentRoom.CustomProperties["complexity"];
    }
    public override void OnJoinedRoom()
    {
        JoinRoomAction?.Invoke();
        _networkUI.CloseAll();
        PlayerData.GameComplexity = (GameComplexity)PhotonNetwork.CurrentRoom.CustomProperties["complexity"];
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _networkUI.CloseAll();
        _errorPanel.openErrorPanel(message);
    }
    public override void OnLeftRoom()
    {
        CloseWaitingMenu?.Invoke();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("_________________");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _networkUI.CloseAll();
        _errorPanel.openErrorPanel(message);
    }
    public override void OnLeftLobby()
    {
        Debug.Log("________________________________________________________________");
    }
    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
    {
        UpdateRoomList?.Invoke(roomList);
    }

    #endregion
}
