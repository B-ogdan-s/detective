using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonMaster : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _regionID;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_regionID);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("1");
    }

    public override void OnConnected()
    {

        Debug.Log("2");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("3");
    }
}
