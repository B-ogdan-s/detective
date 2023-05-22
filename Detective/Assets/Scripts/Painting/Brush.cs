using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{
    [SerializeField, Min(1)] private float _minRadius;
    [SerializeField, Min(1)] private float _maxRadius;
    [SerializeField] private Slider _slider;


    [SerializeField] private Palette[] palettes;

    private float _radius;
    private Color _color;

    public float Radius => _radius;
    public Color Color => _color;

    private void Awake()
    {
        _slider.minValue = _minRadius;
        _slider.maxValue = _maxRadius;

        _slider.value = _minRadius;
        _radius = _minRadius;

        foreach (var p in palettes)
        {
            p.ColorChanged += SetColor;
        }

        palettes[0].UpdateColor();
    }

    private void SetColor(Color newColor)
    {
        _color = newColor;
    }

    public void SetRadius(float newRadius)
    {
        _radius = newRadius;
    }
}
