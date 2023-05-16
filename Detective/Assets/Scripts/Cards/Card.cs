using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private RawImage _background;

    private const string _backgroundPath = "Textures/Background_";
    private CardInfoClass _cadrInfo;

    public void SetInfo(CardInfoClass info)
    {
        _cadrInfo = info;

        _priceText.text = info.Price.ToString();
        _infoText.text = info.Text;



        Texture2D texture = Resources.Load<Texture2D>(_backgroundPath + string.Format("{0:000}", info.BackgroundID));
        _background.texture = texture;
    }


}
