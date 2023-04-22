using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameRoom;
    [SerializeField] private string _addText;

    private Photon.Realtime.RoomInfo _room;

    public Photon.Realtime.RoomInfo GetRoomInfo => _room;

    public void SetRoomInfo(Photon.Realtime.RoomInfo roomInfo)
    {
        _room = roomInfo;

        _nameRoom.text = _addText + " \"" + _room.Name + "\"";
    }
}
