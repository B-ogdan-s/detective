using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class PhotonMaster : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _regionID;

    private NetworkUI _networkUI;
    private bool _isStart = true;

    public Action<List<Photon.Realtime.RoomInfo>> UpdateRoomList;
    public Action JoinRoom;
    public Action CloseWaitingMenu;

    private void Awake()
    {
        _networkUI = GetComponent<NetworkUI>();
        Conect();
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void Conect()
    {
        _networkUI.StartConected();

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_regionID);
    }

    public void CreateRoom(RoomInfo roomInfo)
    {
        Hashtable table = new Hashtable();

        string complexity = roomInfo.GameComplexity.ToString();
        Debug.Log(complexity);

        table.Add("complexity", complexity);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = table;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "complexity" };

        PhotonNetwork.CreateRoom(roomInfo.RoomName, roomOptions);
    }

    
    public void CloseRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region Callbacks

    public override void OnConnectedToMaster()
    {
        _networkUI.Conected();
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

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    public override void OnCreatedRoom()
    {
        JoinRoom?.Invoke();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("0000000000000000");
        CloseWaitingMenu?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
    {
        UpdateRoomList?.Invoke(roomList);
    }

    #endregion
}
