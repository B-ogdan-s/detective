using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private PhotonView _voew;
    [SerializeField] private CardsManager _cardsManager;
    private int _playrsNum = 0;

    #region RPC

    [PunRPC]
    public void CheckTheReadinessOfAllPlayers()
    {
        _playrsNum++;
        Debug.Log(_playrsNum + ") Yes");
        if(_playrsNum == PhotonNetwork.PlayerList.Length)
        {
            _playrsNum = 0;
            _cardsManager.CardDistribution();
        }

    }

    #endregion
}
