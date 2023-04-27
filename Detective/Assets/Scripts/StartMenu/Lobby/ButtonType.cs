using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonType : MonoBehaviour
{
    [SerializeField] GameComplexity _gameType;
    [SerializeField] private Button _button;

    public GameComplexity GameType => _gameType;

    public System.Action<ButtonType> ButtonClick;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            ButtonClick?.Invoke(this);
        });
    }

    public void Change(bool change)
    {
        _button.interactable = change;
    }

}
