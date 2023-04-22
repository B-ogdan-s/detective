using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputRoomInfo : MonoBehaviour
{
    [SerializeField] private PhotonMaster _photonMaster;

    [SerializeField] private TMP_InputField _inputNameRoom;

    private RoomInfo _roomInfo= new RoomInfo();

    private void Awake()
    {
        _inputNameRoom.onEndEdit.AddListener((string name) =>
        {
            _roomInfo.RoomName = name;
        });
        
    }


    public void OnCreateRoom()
    {
        if(string.IsNullOrEmpty(_roomInfo.RoomName))
        {
            Debug.Log("Error");
            return;
        }

        _photonMaster.CreateRoom(_roomInfo);
    }
}

public class RoomInfo
{
    public string RoomName;
    public string RoomPasword;
}

