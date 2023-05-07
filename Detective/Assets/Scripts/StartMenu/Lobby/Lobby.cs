using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class Lobby : MonoBehaviour
{
    [SerializeField] private PhotonMaster _photonMaster;

    [SerializeField] private InputPasswordToRoom _inputPasswordToRooml;
    [SerializeField] private RoomPanel _roomPrefab;
    [SerializeField] private Filter _filter;
    [SerializeField] private ErrorPanel _errorPanel;
    [SerializeField] private Transform _roomParents;

    private Dictionary<string, RoomPanel> _dictionaryRoomInfo = new Dictionary<string, RoomPanel>();


    private void Awake()
    {
        _photonMaster.UpdateRoomList += UpdateRoomList;
        _filter.UpdateFilterAction += RoomFilter;
        _filter.FilterByNameAction += JoinToRoom;
    }

    private void UpdateRoomList(List<Photon.Realtime.RoomInfo> roomList)
    {
        _inputPasswordToRooml.StartSettings(_photonMaster.JoinRoom);

        foreach (Photon.Realtime.RoomInfo room in roomList)
        {
            if(room.RemovedFromList)
            {
                Destroy(_dictionaryRoomInfo[room.Name].gameObject);
                _dictionaryRoomInfo.Remove(room.Name);
                continue;
            }

            if(_dictionaryRoomInfo.ContainsKey(room.Name))
            {
                _dictionaryRoomInfo[room.Name].SetRoomInfo(room);
                continue;
            }

            Debug.Log(room.Name);

            RoomPanel newRoom = Instantiate(_roomPrefab);
            newRoom.transform.SetParent(_roomParents);
            newRoom.transform.localScale = new Vector3(1, 1, 1);
            newRoom.SetRoomInfo(room);
            newRoom.SetButtonAction();
            newRoom.PasswordAction += JoinToRoom;

            _dictionaryRoomInfo.Add(room.Name, newRoom);

            RoomFilter(newRoom);
        }
    }

    private void RoomFilter()
    {
        foreach(var r in _dictionaryRoomInfo)
        {
            r.Value.gameObject.SetActive(_filter.CheckFilter(r.Value.GetRoomInfo));
        }
    }

    private void RoomFilter(RoomPanel room)
    {
        room.gameObject.SetActive(_filter.CheckFilter(room.GetRoomInfo));
    }

    private void JoinToRoom(string roomName)
    {
        if(_dictionaryRoomInfo.ContainsKey(roomName))
        {
            string password = (string)_dictionaryRoomInfo[roomName].GetRoomInfo.CustomProperties["password"];

            if ((bool)_dictionaryRoomInfo[roomName].GetRoomInfo.CustomProperties["isPassword"])
            {
                _inputPasswordToRooml.OpenPassword(password, roomName);
                return;
            }
            _photonMaster.JoinRoom(roomName);
        }
        else
        {
            _errorPanel.openErrorPanel("This room name does not exist");
        }
    }

    private void OnDestroy()
    {
        _photonMaster.UpdateRoomList -= UpdateRoomList;
        _filter.UpdateFilterAction -= RoomFilter;
        _filter.FilterByNameAction -= JoinToRoom;
    }
}
