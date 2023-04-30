using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputPasswordToRoom : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private ErrorPanel _errorPanel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _enterPassword;
    private Canvas _canvas;

    private string _truePassword;
    private string _password;

    private string _roomName;

    public System.Action<string> JoinToRoom;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        _inputPassword.onEndEdit.AddListener((string value) =>
        {
            _password = value;
        });

        _closeButton.onClick.AddListener(() =>
        {
            _inputPassword.text = "";
            _canvas.enabled = false;
        });

        _enterPassword.onClick.AddListener(() =>
        {
            if(_password == _truePassword)
            {
                JoinToRoom?.Invoke(_roomName);
                _inputPassword.text = "";
                _canvas.enabled = false;
            }
            else
            {
                _errorPanel.openErrorPanel("This password does not match");
                _inputPassword.text = "";
            }
        });
    }

    public void StartSettings(System.Action<string> joinToRoom)
    {
        JoinToRoom = joinToRoom;
    }

    public void OpenPassword(string truePassword, string roomName)
    {
        _truePassword = truePassword;
        _roomName = roomName;
        _canvas.enabled = true;
    }

}
