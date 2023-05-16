using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonMasterGameMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private CardsManager _cardsManager;
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _cardsManager.OtherPlayerExit(otherPlayer.UserId);
    }
}
