using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class WaitingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _playersText;
    [SerializeField] private NetworkTimer _timer;
    [SerializeField] private string _sceneName;

    Photon.Realtime.Room _room;

    private byte _minPeople = 0;
    private byte _maxPeople = 0;

    private void Awake()
    {
        _timer.EndTimer += TransitionToTheGameStage;
    }

    public override void OnCreatedRoom()
    {
        StartSettings();
    }
    public override void OnJoinedRoom()
    {
        StartSettings();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateRoomInfo((byte)PhotonNetwork.PlayerList.Length);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoomInfo((byte)PhotonNetwork.PlayerList.Length);
    }

    private void StartSettings()
    {
        _room = PhotonNetwork.CurrentRoom;

        _minPeople = (byte)_room.CustomProperties["minPeople"];
        _maxPeople = _room.MaxPlayers;

        UpdateRoomInfo((byte)PhotonNetwork.PlayerList.Length);
    }

    private void UpdateRoomInfo(byte numPlayers)
    {
        if(_maxPeople == _minPeople)
            _playersText.text = $"{numPlayers}/{_minPeople}";
        else
            _playersText.text = $"{numPlayers}/({_minPeople}-{_maxPeople})";


        if(numPlayers >= _minPeople)
        {
            _timer.StartTimer();
        }
        else
        {
            _timer.StopTimer();
        }

        if(numPlayers == _maxPeople)
        {
            _timer.StopTimer();
            TransitionToTheGameStage();
        }
    }

    private void TransitionToTheGameStage()
    {
        _room.IsVisible = false;
        PhotonNetwork.LoadLevel(_sceneName);
    }

    private void OnDestroy()
    {
        _timer.EndTimer -= TransitionToTheGameStage;
    }

}
