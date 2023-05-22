using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    [SerializeField] private Color _color;

    public System.Action<Color> ColorChanged;

    public void UpdateColor()
    {
        ColorChanged?.Invoke(_color);
    }
}
