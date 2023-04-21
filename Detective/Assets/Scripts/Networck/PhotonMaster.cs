using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PhotonMaster : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _regionID;

    private NetworkUI _networkUI;
    private bool _isStart = true;

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

    public void CreateRoom(string nameRoom)
    {
        PhotonNetwork.CreateRoom(nameRoom);
    }

    #region Callbacks

    public override void OnConnectedToMaster()
    {
        _networkUI.Conected();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if(_isStart)
        {
            _isStart = false;
            return;
        }

        Debug.Log(cause);

        _networkUI.Disconnected(cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    #endregion
}
