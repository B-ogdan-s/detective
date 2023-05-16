using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardsManager : MonoBehaviour
{
    [SerializeField, Min(0)] private int _numberOfIssuedCards;
    [SerializeField] private ListCards _listOfPlayerCards;
    [SerializeField] private ListCards _listOfFreeCards;
    [SerializeField] private PhotonView _photonView;

    private List<CardInfoClass> _cardsList = new List<CardInfoClass>();
    private List<CardInfoClass> _freeCardList = new List<CardInfoClass>();
    private List<CardInfoClass> _exidCardsList = new List<CardInfoClass>();

    private Dictionary<string, List<CardInfoClass>> _playerCards = new Dictionary<string, List<CardInfoClass>>();

    private const string _pathCard = "Info/EvidenceCards";
   
    public void CardDistribution()
    {
        _photonView.RPC("RPC_ClearPlayerCards", RpcTarget.All);

        foreach(var player in PhotonNetwork.PlayerList)
        {
            List<CardInfoClass> cards = new List<CardInfoClass>();
            for(int i = 0; i < _numberOfIssuedCards; i++)
            {
                int id = Random.Range(0, _cardsList.Count);
                cards.Add(_cardsList[id]);
                _cardsList.Remove(cards[i]);
            }
            CardInfoClass[] cardsArray = cards.ToArray(); 

            GameInfo newGameInfo = new GameInfo();
            newGameInfo.CardInfos = cardsArray;

            string jsonFile = JsonUtility.ToJson(newGameInfo);
            _photonView.RPC("RPC_SetPlayerCardInfo", RpcTarget.All, player.UserId, jsonFile);

        }
    }

    public void OtherPlayerExit(string playerId)
    {
        _freeCardList.AddRange(_playerCards.GetValueOrDefault(playerId));
        _playerCards.Remove(playerId);
        _listOfFreeCards.SetNewList(_freeCardList);
    }


    #region RPC

    [PunRPC]
    private void RPC_SetGameInfo(string jsonGameInfo)
    {
        GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(jsonGameInfo);

        foreach(var card in gameInfo.CardInfos)
        {
            _cardsList.Add(card);
        }

        foreach(var player in PhotonNetwork.PlayerList)
        {
            _playerCards.Add(player.UserId, new List<CardInfoClass>());
        }

        _photonView.RPC("CheckTheReadinessOfAllPlayers", RpcTarget.MasterClient);

    }

    [PunRPC]
    private void RPC_SetPlayerCardInfo(string playerId, string jsonCardInfo)
    {
        GameInfo cardInfoClasses = JsonUtility.FromJson<GameInfo>(jsonCardInfo);
        _playerCards.Remove(playerId);
        List<CardInfoClass> cards = new List<CardInfoClass>();
        foreach (CardInfoClass cardInfoClass in cardInfoClasses.CardInfos)
            cards.Add(cardInfoClass);
        _playerCards.Add(playerId, cards);


        if(playerId == PhotonNetwork.LocalPlayer.UserId)
        {
            _listOfPlayerCards.SetNewList(cards);
        }    

    }
    [PunRPC]
    private void RPC_ClearPlayerCards()
    {
        foreach(var player in PhotonNetwork.PlayerList)
        {
            List<CardInfoClass> cardInfoClasses = _playerCards.GetValueOrDefault(player.UserId);
            foreach (var card in cardInfoClasses)
                _exidCardsList.Add(card);
        }
    }

    #endregion
}
