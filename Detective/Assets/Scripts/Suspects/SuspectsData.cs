using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuspectData", menuName = "Suspect/SuspectData")]
public class SuspectsData : ScriptableObject
{
    [SerializeField] private Texture2D _suspectsModel;
    [Tooltip("<b> text </b> - make text thick. \n" +
        "<i> text </i> - make text italic. \n" +
        "<color=colorText> text </color> - make text a specific color.")]
    [SerializeField] private string _name;
    [Tooltip("example: \" 01.01.1980 \"")]
    [SerializeField] private string _dateOfBirth;
    [SerializeField] private Motive[] _motives;

    public string Name => _name;
    public string DateOfBirth => _dateOfBirth;
}

[System.Serializable]
public class Motive
{
    [SerializeField] private Texture2D _motiveIcon;
    [SerializeField] private string _motiveText;

    public Texture2D MotiveIcon => _motiveIcon;
    public string MotiveText => _motiveText;
}
