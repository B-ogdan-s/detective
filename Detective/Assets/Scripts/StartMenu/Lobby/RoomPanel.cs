using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameRoom;
    [SerializeField] private TextMeshProUGUI _complexity;
    [SerializeField] private TextMeshProUGUI _numberPeople;
    [SerializeField] private TextMeshProUGUI _minAndMaxPeople;

    [SerializeField] private Image _lockIcon;
    [SerializeField] private string _addTextToRoomName;
    [SerializeField] private string _addTextToRoomComplexty;

    [SerializeField] private Button _joinToRoomButton;

    private Photon.Realtime.RoomInfo _room;

    public System.Action<string> PasswordAction;

    public Photon.Realtime.RoomInfo GetRoomInfo => _room;

    public void SetRoomInfo(Photon.Realtime.RoomInfo roomInfo)
    {
        _room = roomInfo;

        _nameRoom.text = _addTextToRoomName + "\n\"" + _room.Name + "\"";
        _numberPeople.text = _room.PlayerCount.ToString();

        GameComplexity g = (GameComplexity)_room.CustomProperties["complexity"];

        _complexity.text = _addTextToRoomComplexty + "\n\"" + g.ToString() + "\"";
        _minAndMaxPeople.text = (byte)_room.CustomProperties["minPeople"] + "-" + _room.MaxPlayers;

        _lockIcon.enabled = (bool)_room.CustomProperties["isPassword"];
    }

    public void SetButtonAction()
    {
        _joinToRoomButton.onClick.AddListener(() =>
        {
            PasswordAction?.Invoke(_room.Name);
        });
    }

    private void OnDestroy()
    {
        _joinToRoomButton.onClick.RemoveAllListeners();
    }
}
