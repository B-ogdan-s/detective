using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfPlayerCardsUI : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private Transform _parents;
    [SerializeField] private Canvas _canvas;

    private PoolSystem<Card> _cardsPool;

    private void Awake()
    {
        _cardsPool = new PoolSystem<Card>(_cardPrefab, _parents);
        Close();
    }

    public void Open()
    {
        _canvas.enabled = true;
    }

    public void Close()
    {
        _canvas.enabled = false;
        _cardsPool.DisablePool();
    }

    public void SpawnCards(List<CardInfoClass> cardsInfo)
    {
        foreach (CardInfoClass cardinfo in cardsInfo)
        {
            Card newCard = _cardsPool.GetPool();
            newCard.SetInfo(cardinfo);
        }
    }
}
