using Photon.Pun;
using UnityEngine;

public class CrimeManager : MonoBehaviour
{
    private CrimeInfo[] _crimesList;
    private const string _pathCrime = "Info/Crime";
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        string path = _pathCrime;
        if(PlayerData.GameComplexity != GameComplexity.Random)
        {
            path += "/" + PlayerData.GameComplexity.ToString();
        }

        _crimesList = Resources.LoadAll<CrimeInfo>(path);

        if(PhotonNetwork.IsMasterClient)
        {
            SelectCrime();
        }
    }

    private void SelectCrime()
    {
        int index = Random.Range(0, _crimesList.Length);
        CrimeInfo _gameCrime = _crimesList[index];

        GameInfo gameInfo = new GameInfo();

        gameInfo.CardInfos = new CardDataClass[_gameCrime.CardInfoClasses.Length];
        
        for(int i = 0; i < _gameCrime.CardInfoClasses.Length; i++)
        {
            gameInfo.CardInfos[i] = _gameCrime.CardInfoClasses[i].CardInfoClass;
        }

        //_gameCrime.CardInfoClasses;

        string jsonGameInfo = JsonUtility.ToJson(gameInfo);
        _photonView.RPC("RPC_SetGameInfo", RpcTarget.All, jsonGameInfo);

    }
}
public class GameInfo
{
    public CardDataClass[] CardInfos;

}

