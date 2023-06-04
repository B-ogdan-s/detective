using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfSuspects : MonoBehaviour
{
    [SerializeField] private SuspectsData[] _suspectsDatas;

    [SerializeField] private SuspectInfoPanel _infoPanel;
    [SerializeField] private SuspectInfoPanelUI _infoPanelUI;

    [SerializeField] private Painting _painting;

    [SerializeField] private Transform _parents;
    [SerializeField] private SuspectMiniPanel _miniPanelPrefab;

    private TexturePattern[] _texturesPattern;

    private sbyte _suspectsIndex = 0;

    private void Awake()
    {
        _texturesPattern = new TexturePattern[_suspectsDatas.Length];

        _infoPanel.CloseAction += Close;

        for(sbyte i = 0; i <_suspectsDatas.Length; i++)
        {
            SuspectMiniPanel miniPanel = Instantiate(_miniPanelPrefab);
            miniPanel.transform.SetParent(_parents);
            miniPanel.Spawn(i, OpenSuspectData);
        }
    }

    private void OpenSuspectData(sbyte index)
    {
        _suspectsIndex = index;
        _painting.Texture = _texturesPattern[_suspectsIndex];
        _infoPanel.Open();
        _infoPanelUI.UpdateData(_suspectsDatas[_suspectsIndex], _suspectsIndex);
    }
    public void Change(int value)
    {
        _texturesPattern[_suspectsIndex] = _painting.Texture;

        _suspectsIndex += (sbyte)value;

        if (_suspectsIndex < 0)
            _suspectsIndex = (sbyte)(_suspectsDatas.Length - 1);
        else if(_suspectsIndex >= _suspectsDatas.Length)
            _suspectsIndex = 0;

        _painting.Texture = _texturesPattern[_suspectsIndex];

        _infoPanelUI.UpdateData(_suspectsDatas[_suspectsIndex], _suspectsIndex);
    }

    private void Close()
    {
        _texturesPattern[_suspectsIndex] = _painting.Texture;
    }
}
