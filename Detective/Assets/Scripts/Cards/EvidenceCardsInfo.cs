using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Evidence Cards", menuName = "Cards/Evidence Cards")]
public class EvidenceCardsInfo : ScriptableObject
{
    [SerializeField] private ushort _id;
    [SerializeField, Min(1)] private byte _price;
    [SerializeField] private string _text;
    [SerializeField] private TypeOfEvidence _typeOfEvidence;
}

[Flags]
public enum TypeOfEvidence
{
    None = 0,
    P0 = 1,
    P1 = 2,
    P2 = 4,
    P3 = 8,
    P4 = 16,
    P5 = 32,
}

