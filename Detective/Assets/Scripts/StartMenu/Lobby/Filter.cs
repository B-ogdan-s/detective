using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputName;
    [SerializeField] private TwinSlider _twinSlider;
    [SerializeField] private ComplexityButton _complexityButton;
    [SerializeField] private Toggle _toggleShowPrivate;

    private Canvas _canvas;

    private string _nameFilter;

    private byte _minPerson;
    private byte _maxPerson;
    private bool _isPrivate;
    private GameComplexity _gameComplexity;

    public System.Action UpdateFilterAction;
    public System.Action<string> FilterByNameAction;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _inputName.onEndEdit.AddListener((string value) =>
        {
            _nameFilter = value;
        });

        _complexityButton.Clear();
        _toggleShowPrivate.SetIsOnWithoutNotify(true);
        _twinSlider.Clear(_minPerson, _maxPerson);
        _canvas.enabled = false;
    }


    private void Start()
    {
        UpdateFilter();
    }

    public void OpenFilter()
    {
        _canvas.enabled = true;
    }

    public void CloseFilter()
    {
        _complexityButton.Clear(_gameComplexity);
        _toggleShowPrivate.SetIsOnWithoutNotify(_isPrivate);
        _twinSlider.Clear(_minPerson, _maxPerson);

        _nameFilter = "";
        _inputName.text = "";
        _canvas.enabled = false;
    }

    public bool CheckFilter(Photon.Realtime.RoomInfo roomInfo)
    {
        byte minPerson = (byte)roomInfo.CustomProperties["minPeople"];
        byte maxPerson = roomInfo.MaxPlayers;
        bool isPrivate = string.IsNullOrEmpty((string)roomInfo.CustomProperties["password"]);
        GameComplexity gameComplexity = (GameComplexity)roomInfo.CustomProperties["complexity"];

        if(_isPrivate == false && isPrivate == false)
        {
            Debug.Log("1");
            return false;
        }
        else if(_gameComplexity != GameComplexity.All && _gameComplexity != gameComplexity)
        {
            Debug.Log("2");
            return false;
        }
        else if(_minPerson > minPerson || _maxPerson < maxPerson)
        {
            Debug.Log("3");
            return false;
        }

        return true;
    }

    public void FilterByName()
    {
        if (string.IsNullOrEmpty(_nameFilter))
            return;

        FilterByNameAction?.Invoke(_nameFilter);
        CloseFilter();
    }

    public void UpdateFilter()
    {
        _minPerson = _twinSlider.MinValue;
        _maxPerson = _twinSlider.MaxValue;
        _isPrivate = _toggleShowPrivate.isOn;
        _gameComplexity = _complexityButton.GetGameComplexity;

        _nameFilter = "";
        _inputName.text = "";

        UpdateFilterAction?.Invoke();

        _canvas.enabled = false;
    }
}
