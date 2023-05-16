using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfFreeCards : ListCards
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;

    public override void SetNewList(List<CardInfoClass> cardInfoClasses)
    {
        base.SetNewList(cardInfoClasses);
        _text.text = cardInfoClasses.Count.ToString();

    }
}
