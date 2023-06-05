using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerDataMiniPanel _playerDataPrefab;
    [SerializeField] private Transform _parents;

    private List<PlayerDataMiniPanel> _players = new List<PlayerDataMiniPanel>();

    private void Awake()
    {
        foreach(var p in PhotonNetwork.PlayerListOthers)
        {
            PlayerDataMiniPanel playerData = Instantiate(_playerDataPrefab);
            playerData.transform.SetParent(_parents);
            playerData.SetFirstPlayer(false);
            playerData.PlayerId = p.UserId;
            _players.Add(playerData);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for(int i = 0; i < _players.Count; i++)
        {
            if (_players[i].PlayerId == otherPlayer.UserId)
            {
                Destroy(_players[i].gameObject);
                _players.RemoveAt(i);
                break;
            }
        }
    }

    public void SetFirstPlayer(string playerId)
    {
        foreach (var p in _players)
        {
            if (p.PlayerId == playerId)
                p.SetFirstPlayer(true);
            else
                p.SetFirstPlayer(false);
        }
    }

}
