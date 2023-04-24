using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputRoomInfo : MonoBehaviour
{
    [SerializeField] private PhotonMaster _photonMaster;

    [SerializeField] private TMP_InputField _inputNameRoom;
    [SerializeField] private ButtonType[] _buttonsList;

    private RoomInfo _roomInfo= new RoomInfo();

    private void Awake()
    {
        _inputNameRoom.onEndEdit.AddListener((string name) =>
        {
            _roomInfo.RoomName = name;
        });
        
        foreach(var button in _buttonsList)
        {
            button.ButtonClick += OnButtonClick;
        }

    }

    private void OnButtonClick(GameComplexity type, ButtonType button)
    {
        _roomInfo.GameComplexity = type;
        foreach (var b in _buttonsList)
        {
            b.Change(true);
        }
        button.Change(false);
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

    private void OnDestroy()
    {
        foreach (var button in _buttonsList)
        {
            button.ButtonClick -= OnButtonClick;
        }
    }
}

//[Flags]
public enum GameComplexity
{
    Easy = 1,
    Normal = 2,
    Hard = 4,
    Random = 7
}

public class RoomInfo
{
    public string RoomName;
    public string RoomPasword;
    public GameComplexity GameComplexity;
}

