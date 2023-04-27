using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputRoomInfo : MonoBehaviour
{
    [SerializeField] private PhotonMaster _photonMaster;
    [SerializeField] private TwinSlider _twinSlider; 
    [SerializeField] private TMP_InputField _inputNameRoom;
    [SerializeField] private TMP_InputField _inputPasswordRoom;

    [SerializeField] private ButtonType[] _buttonsList;

    private RoomInfo _roomInfo= new RoomInfo();

    private void Awake()
    {
        _twinSlider.OnSliderChange += InputSliderValue;

        _inputNameRoom.onEndEdit.AddListener((string name) =>
        {
            _roomInfo.RoomName = name;
        });

        _inputPasswordRoom.onEndEdit.AddListener((string password) =>
        {
            _roomInfo.RoomPasword = password;
        });

        foreach (var button in _buttonsList)
        {
            button.ButtonClick += OnButtonClick;
        }

        OnButtonClick(_buttonsList[_buttonsList.Length - 1]);
    }

    private void OnButtonClick( ButtonType button)
    {
        _roomInfo.GameComplexity = button.GameType;
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

    private void InputSliderValue(byte min, byte max)
    {
        _roomInfo.MinPeople = min;
        _roomInfo.MaxPeople = max;
    }

    private void OnDestroy()
    {
        foreach (var button in _buttonsList)
        {
            button.ButtonClick -= OnButtonClick;
        }

        _twinSlider.OnSliderChange -= InputSliderValue;
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
    public GameComplexity GameComplexity;
    public string RoomPasword = "";

    public byte MinPeople;
    public byte MaxPeople;

}

