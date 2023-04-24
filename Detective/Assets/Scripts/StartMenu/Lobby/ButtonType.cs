using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonType : MonoBehaviour
{
    [SerializeField] GameComplexity _gameType;
    private Button _button;

    public System.Action<GameComplexity, ButtonType> ButtonClick;

    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() =>
        {
            ButtonClick?.Invoke(_gameType, this);
        });
    }

    public void Change(bool change)
    {
        _button.interactable = change;
    }

}
