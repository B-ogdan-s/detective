using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private PhotonMaster _photonMaster;

    [SerializeField] private Room _roomPrefab;
    [SerializeField] private Transform _roomParents;

    private Dictionary<string, Room> _dictionaryRoomInfo = new Dictionary<string, Room>();


    private void Awake()
    {
        _photonMaster.UpdateRoomList += UpdateRoomList;
    }

    private void UpdateRoomList(List<Photon.Realtime.RoomInfo> roomList)
    {
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

            Room newRoom = Instantiate(_roomPrefab);
            newRoom.transform.SetParent(_roomParents);
            newRoom.transform.localScale = new Vector3(1, 1, 1);

            newRoom.SetRoomInfo(room);
            _dictionaryRoomInfo.Add(room.Name, newRoom);
        }
    }

    private void OnDestroy()
    {
        _photonMaster.UpdateRoomList -= UpdateRoomList;
    }
}
