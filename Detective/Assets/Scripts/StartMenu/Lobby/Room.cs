using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameRoom;
    [SerializeField] private TextMeshProUGUI _complexity;
    [SerializeField] private TextMeshProUGUI _numberPeople;
    [SerializeField] private string _addTextToRoomName;
    [SerializeField] private string _addTextToRoomComplexty;

    private Photon.Realtime.RoomInfo _room;

    public Photon.Realtime.RoomInfo GetRoomInfo => _room;

    public void SetRoomInfo(Photon.Realtime.RoomInfo roomInfo)
    {
        _room = roomInfo;

        _nameRoom.text = _addTextToRoomName + "\n\"" + _room.Name + "\"";
        _numberPeople.text = _room.PlayerCount.ToString();

        string g = (string)_room.CustomProperties["complexity"];

        Debug.Log(_room.CustomProperties["complexity"]);

        _complexity.text = _addTextToRoomComplexty + "\n\"" + g + "\"";
    }
}
