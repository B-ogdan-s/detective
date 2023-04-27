using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameRoom;
    [SerializeField] private TextMeshProUGUI _complexity;
    [SerializeField] private TextMeshProUGUI _numberPeople;
    [SerializeField] private string _addTextToRoomName;
    [SerializeField] private string _addTextToRoomComplexty;

    [SerializeField] private Button _joinToRoomButton;

    private Photon.Realtime.RoomInfo _room;

    public System.Action<string, string> PasswordAction;

    public Photon.Realtime.RoomInfo GetRoomInfo => _room;

    public void SetRoomInfo(Photon.Realtime.RoomInfo roomInfo)
    {
        _room = roomInfo;

        _nameRoom.text = _addTextToRoomName + "\n\"" + _room.Name + "\"";
        _numberPeople.text = _room.PlayerCount.ToString();

        string g = (string)_room.CustomProperties["complexity"];

        _complexity.text = _addTextToRoomComplexty + "\n\"" + g + "\"";
    }

    public void InstaniateButton(System.Action<string> joinRoom)
    {
        string password = (string)_room.CustomProperties["password"];

        Debug.Log(password);

        _joinToRoomButton.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(password))
            {

                joinRoom.Invoke(_room.Name);
                return;
            }

            PasswordAction?.Invoke(password, _room.Name);

        });

    }

    private void OnDestroy()
    {
        _joinToRoomButton.onClick.RemoveAllListeners();
    }
}
