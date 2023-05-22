using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapTransform : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private float _maxPosX;
    [SerializeField] private float _maxPosY;
    [SerializeField] private float _speedMove;

    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField, Min(0)] private float _sizeSpeed;

    [SerializeField] private RectTransform _transform;

    private List<PointerEventData> _pointerEventDatas = new List<PointerEventData>();
    private float _oldDistans;
    private Vector2 _oldPos;


    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerEventDatas.Add(eventData);

        if(_pointerEventDatas.Count == 1)
        {
            _oldPos = eventData.position;
        }
        else if(_pointerEventDatas.Count == 2)
        {
            float x = _pointerEventDatas[0].position.x - _pointerEventDatas[1].position.x;
            float y = _pointerEventDatas[0].position.y - _pointerEventDatas[1].position.y;
            _oldDistans = Mathf.Sqrt(x * x + y * y);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerEventDatas.Remove(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(_pointerEventDatas.Count == 1)
        {
            UpdatePos(eventData.position - _oldPos);
            _oldPos = eventData.position;
        }
        else if (_pointerEventDatas.Count == 2)
        {
            float x = _pointerEventDatas[0].position.x - _pointerEventDatas[1].position.x;
            float y = _pointerEventDatas[0].position.y - _pointerEventDatas[1].position.y;
            float distans = Mathf.Sqrt(x * x + y * y);
            UpdateScale(distans);
            _oldDistans = distans;
        }
    }

    private void UpdateScale(float distans)
    {
        float delta = (distans - _oldDistans) * _sizeSpeed * Time.deltaTime;
        if(_transform.localScale.x + delta < _minSize)
        {
            _transform.localScale = _minSize * Vector3.one;
        }
        else if(_transform.localScale.x + delta > _maxSize)
        {
            _transform.localScale = _maxSize * Vector3.one;
        }
        else
        {
            _transform.localScale += delta * Vector3.one;
        }
    }
    private void UpdatePos(Vector2 direction)
    {
        Vector2 newPos = (Vector2)_transform.localPosition + direction * Time.deltaTime * _speedMove;

        if(Mathf.Abs(newPos.x) > _maxPosX)
        {
            newPos.x = newPos.x / Mathf.Abs(newPos.x) * _maxPosX;
        }
        if (Mathf.Abs(newPos.y) > _maxPosY)
        {
            newPos.y = newPos.y / Mathf.Abs(newPos.y) * _maxPosY;
        }

        _transform.localPosition = newPos;
    }

    public void Close()
    {
        _transform.localPosition = Vector3.zero;
        _transform.localScale = Vector3.one;
    }
}
