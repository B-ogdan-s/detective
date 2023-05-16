using UnityEngine;

[CreateAssetMenu(fileName = "Evidence Cards", menuName = "Cards/Evidence Cards")]
public class EvidenceCardsInfo : ScriptableObject
{
    [SerializeField] private CardInfoClass _cardInfoClass;

    public CardInfoClass CardInfoClass => _cardInfoClass;
}


[System.Serializable]
public class CardInfoClass
{
    [Min(1)] public byte Price;

    [TextArea(6, 15)]
    [Tooltip("<b> text </b> - make text thick. \n" +
        "<i> text </i> - make text italic. \n" +
        "<color=colorText> text </color> - make text a specific color.")]
    public string Text;
    [Min(1)] public byte BackgroundID;
}

