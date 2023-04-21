using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private Canvas _createRoom;

    private void Awake()
    {
        CloseCreateRoom();
    }

    public void OpenCreateRoom()
    {
        _createRoom.enabled = true;
    }
    public void CloseCreateRoom()
    {
        _createRoom.enabled = false;
    }

}
