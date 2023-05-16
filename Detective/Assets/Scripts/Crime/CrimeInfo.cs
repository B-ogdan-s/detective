using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "CrimeInfo", menuName = "Crime/CrimeInfo")]
public class CrimeInfo : ScriptableObject
{
    [SerializeField] private EvidenceCardsInfo[] _cardInfos;

    public EvidenceCardsInfo[] CardInfoClasses => _cardInfos;
}