using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private PhotonView _voew;
    [SerializeField] private CardsManager _cardsManager;
    [SerializeField] private NetworkTimer _timer;
    [SerializeField] private PlayerList _players;

    [SerializeField] private byte _timeToCheckTheCards;

    [SerializeField] private byte _firstPlayer = 0;


    private int _playrsNum = 0;

    #region RPC

    [PunRPC]
    public void CheckTheReadinessOfAllPlayers()
    {
        _playrsNum++;
        if(_playrsNum == PhotonNetwork.PlayerList.Length)
        {
            _playrsNum = 0;
            _cardsManager.CardDistribution();

            Debug.LogError("_______________________");

            _firstPlayer = (byte)Random.Range(0, PhotonNetwork.PlayerList.Length);

            _voew.RPC("RPC_SetFirstPlayer", RpcTarget.All, _firstPlayer);

            _voew.RPC("RPC_StartTimer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_SetFirstPlayer(params GameObject[] value)
    {
        _players.SetFirstPlayer(PhotonNetwork.PlayerList[value[0].ConvertTo<int>()].UserId);
    }

    [PunRPC]
    private void RPC_StartTimer()
    {
        _timer.TimerValue = _timeToCheckTheCards;
        _timer.StartTimer();
    }
    #endregion
}
