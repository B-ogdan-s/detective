using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataMiniPanel : MonoBehaviour
{
    [SerializeField] private Image _firstPlayerIcon;

    [HideInInspector] public string PlayerId;

    public void SetFirstPlayer(bool value)
    {
        _firstPlayerIcon.enabled = value;
    }
}
