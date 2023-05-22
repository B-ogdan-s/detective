using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTransformManager : MonoBehaviour
{
    [SerializeField] private Button _button;

    [SerializeField] private MapTransform _mapTransform;
    [SerializeField] private Painting _painting;

    private void Awake()
    {
        OpenPainting();
    }

    private void OpenTransform()
    {
        _painting.enabled = false;
        _painting.GetComponent<RawImage>().raycastTarget = false;
        _mapTransform.enabled = true;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OpenPainting);
    }
    private void OpenPainting()
    {
        _painting.enabled = true;
        _painting.GetComponent<RawImage>().raycastTarget = true;
        _mapTransform.enabled = false;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OpenTransform);
    }
}
