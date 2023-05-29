using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "CrimeInfo", menuName = "Crime/CrimeInfo")]
public class CrimeInfo : ScriptableObject
{
    [SerializeField] private EvidenceCardData[] _cardInfos;

    public EvidenceCardData[] CardInfoClasses => _cardInfos;
}
