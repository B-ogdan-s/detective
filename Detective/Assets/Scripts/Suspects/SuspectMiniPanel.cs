using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspectMiniPanel : MonoBehaviour
{
    [SerializeField] private Button _buton;

    private sbyte _index = 0;

    public void Spawn(sbyte index, System.Action<sbyte> action)
    {
        _index = index;
        _buton.onClick.AddListener(() =>
        {
            action?.Invoke(_index);
        });
    }
}
