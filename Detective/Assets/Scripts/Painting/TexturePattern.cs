using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TexturePattern
{
    private Texture2D _texture;
    private float _compression;

    private int _width;
    private int _height;
    private float _radius;
    private Color _color;

    public Texture2D Texture => _texture;

    public TexturePattern(int width, int height, float compression)
    {
        _compression = compression;

        _width = (int)(width * _compression);
        _height = (int)(height * _compression);

        _texture = new Texture2D(_width, _height);
        _texture.filterMode = FilterMode.Point;
        _texture.wrapMode = TextureWrapMode.Clamp;

        for(int i=0; i < _width; i++)
        {
            for(int j=0; j < _height; j++)
            {
                _texture.SetPixel(i, j, new Color(0,0,0,0));
            }
        }
        _texture.Apply();
    }

    public void UpdateBrushInfo(float radius, Color color)
    {
        _radius = radius;
        _color = color;
    }

    public void DrawALine(Vector2 start, Vector2 end)
    {
        Vector2 delta = end - start;

        int deltaStep = (int)(Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y) / (_radius)) + 1;

        float x = delta.x / deltaStep;
        float y = delta.y / deltaStep;

        for (int i = 1; i <= deltaStep; i++)
        {
            //int w = (int)((start.x + (x * i)) * _compression + (_width / 2));
            //int h = (int)((start.y + (y * i)) * _compression + (_height / 2));
            DrawAPoint((int)(start.x + (x * i)), (int)(start.y + (y * i)));
        }

    }

    public void DrawAPoint(int posX, int posY)
    {
        posX = (int)(posX * _compression + (_width / 2));
        posY = (int)(posY * _compression + (_height / 2));

        float r = _radius * _compression;

        int startX = posX - (int)r;
        int startY = posY - (int)r;

        int endX = posX + (int)r;
        int endY = posY + (int)r;

        for (int i = startX; i <= endX; i++)
        {
            for (var j = startY; j <= endY; j++)
            {
                if (CheckRadius(posX - i, posY - j, r))
                {
                    _texture.SetPixel(i, j, _color);
                }
            }
        }
    }

    public void ApplyTexture()
    {
        _texture.Apply();
    }

    private bool CheckRadius(int w, int h, float r)
    {
        float d = Mathf.Sqrt(w * w + h * h);
        if (d < r)
            return true;

        return false;
    }
}
