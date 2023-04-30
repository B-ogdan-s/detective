using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexityButton : MonoBehaviour
{
    [SerializeField] private ButtonType[] _buttonsList;
    private GameComplexity _gameComplexity;

    public GameComplexity GetGameComplexity => _gameComplexity;

    private void Awake()
    {
        foreach (var button in _buttonsList)
        {
            button.ButtonClick += OnButtonClick;
        }

        Clear();
    }

    private void OnButtonClick(ButtonType button)
    {
        _gameComplexity = button.GameType;
        foreach (var b in _buttonsList)
        {
            b.Change(true);
        }
        button.Change(false);
    }

    public void Clear()
    {
        OnButtonClick(_buttonsList[_buttonsList.Length - 1]);
    }

    public void Clear(GameComplexity gameComplexity)
    {
        foreach(var button in _buttonsList)
        {
            if(button.GameType == gameComplexity)
            {
                OnButtonClick(button);
                return;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var button in _buttonsList)
        {
            button.ButtonClick -= OnButtonClick;
        }
    }
}
