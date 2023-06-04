using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuspectInfoPanelUI : MonoBehaviour
{
    [SerializeField] private string _textAddedToIndex;
    [SerializeField] private string _textAddedToName;
    [SerializeField] private string _textAddedToDateOfBirth;

    [SerializeField] private TextMeshProUGUI _indexText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dataOfBirthText;

    public void UpdateData(SuspectsData data, sbyte index)
    {
        _indexText.text = _textAddedToIndex + (index + 1).ToString();
        _nameText.text = _textAddedToName + " " + data.Name;
        _dataOfBirthText.text = _textAddedToDateOfBirth + " " + data.DateOfBirth;
    }
}
