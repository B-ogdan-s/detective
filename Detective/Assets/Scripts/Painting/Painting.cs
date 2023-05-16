using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    [SerializeField, Min(1)] private int _sterWidth;
    [SerializeField, Min(1)] private int _startHeight;
    [SerializeField, Range(0.1f, 1f)] private float _compression;
    [SerializeField] private RawImage _rawImage;

    [SerializeField] private int _width;
    [SerializeField] private int _height;
    private Texture2D _texture;

    private void Awake()
    {
        _width = (int)(_sterWidth * _compression);
        _height = (int)(_startHeight * _compression);

        _texture = new Texture2D(_width, _height);
        for(int i=0; i < _width; i++)
        {
            for(int j=0; j < _height; j++)
            {
                _texture.SetPixel(i, j, new Color(0,0,0,0));
            }
        }
        _texture.Apply();
        _rawImage.texture = _texture;
    }
}
