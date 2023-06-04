using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Painting : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField, Min(1)] private int _width;
    [SerializeField, Min(1)] private int _height;
    [SerializeField, Range(0.1f, 2f)] private float _compression;
    [SerializeField] private RawImage[] _rawImages;
    [SerializeField] private RectTransform _sizeRectTransform;

    [SerializeField] private Brush _brush;

    private TexturePattern _texturePattern;

    private Vector2 _oldPos;
    private RectTransform _rectTransform;

    public TexturePattern Texture
    {
        set
        {
            if(value == null)
                _texturePattern = new TexturePattern(_width, _height, _compression);
            else
                _texturePattern = value;

            foreach (var rawIm in _rawImages)
                rawIm.texture = _texturePattern.Texture;
        }
        get { return _texturePattern; }
    }

    private void Awake()
    {
        if(gameObject.TryGetComponent(out RectTransform rectTransform))
        {
            _rectTransform = rectTransform;
        }
        else
        {
            this.enabled = false;
        }

        _texturePattern = new TexturePattern(_width, _height, _compression);

        foreach(var rawIm in _rawImages)
            rawIm.texture = _texturePattern.Texture;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _texturePattern.UpdateBrushInfo(_brush.Radius, _brush.Color);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, null, out Vector2 pos))
        {
            _texturePattern.DrawAPoint((int)pos.x, (int)pos.y);
            _oldPos = pos;

            _texturePattern.ApplyTexture();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, null, out Vector2 pos))
        {
            _texturePattern.DrawALine(_oldPos, pos);
            _oldPos = pos;

            _texturePattern.ApplyTexture();
        }
    }
}
