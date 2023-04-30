using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _errorText;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CloseErrorPanel();
    }

    public void openErrorPanel(string errorText)
    {
        _errorText.text = errorText;
        _canvas.enabled = true;
    }

    public void CloseErrorPanel()
    {
        _canvas.enabled = false;
    }
}
