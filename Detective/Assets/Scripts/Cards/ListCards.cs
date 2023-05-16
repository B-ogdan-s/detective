using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListCards : MonoBehaviour
{
    [SerializeField] protected List<CardInfoClass> _cardInfoClasses;
    [SerializeField] protected ListOfPlayerCardsUI _ui;

    public virtual void Open()
    {
        _ui.SpawnCards(_cardInfoClasses);
        _ui.Open();
    }
    public virtual void SetNewList(List<CardInfoClass> cardInfoClasses)
    {
        _cardInfoClasses = cardInfoClasses;
    }


}
