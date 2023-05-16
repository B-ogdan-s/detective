using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputRoomInfo : MonoBehaviour
{
    [SerializeField] private PhotonMasterStartMenu _photonMaster;
    [SerializeField] private TwinSlider _twinSlider; 
    [SerializeField] private TMP_InputField _inputNameRoom;
    [SerializeField] private TMP_InputField _inputPasswordRoom;

    [SerializeField] private ComplexityButton _complexityButton;

    private RoomInfo _roomInfo= new RoomInfo();

    private void Awake()
    {
        _inputNameRoom.onEndEdit.AddListener((string name) =>
        {
            _roomInfo.RoomName = name;
        });

        _inputPasswordRoom.onEndEdit.AddListener((string password) =>
        {
            _roomInfo.RoomPasword = password;
        });
    }

    public void OnCreateRoom()
    {

        if (string.IsNullOrEmpty(_roomInfo.RoomName))
        {
            Debug.Log("Error");
            return;
        }


        _roomInfo.MinPeople = _twinSlider.MinValue;
        _roomInfo.MaxPeople = _twinSlider.MaxValue;

        _roomInfo.GameComplexity = _complexityButton.GetGameComplexity;

        _roomInfo.isPassword = !string.IsNullOrEmpty(_roomInfo.RoomPasword);

        _photonMaster.CreateRoom(_roomInfo);
    }
}

//[Flags]
public enum GameComplexity
{
    Easy,
    Normal,
    Hard,
    Random,
    All
}

public class RoomInfo
{
    public string RoomName;
    public GameComplexity GameComplexity;
    public bool isPassword;
    public string RoomPasword = "";

    public byte MinPeople;
    public byte MaxPeople;

}

