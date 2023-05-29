using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListCards : MonoBehaviour
{
    [SerializeField] protected List<CardDataClass> _cardInfoClasses;
    [SerializeField] protected ListOfPlayerCardsUI _ui;

    public virtual void Open()
    {
        _ui.SpawnCards(_cardInfoClasses);
        _ui.Open();
    }
    public virtual void SetNewList(List<CardDataClass> cardInfoClasses)
    {
        _cardInfoClasses = cardInfoClasses;
    }


}
