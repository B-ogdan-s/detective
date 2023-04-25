using System;
using UnityEngine;
using UnityEngine.UI;

public class TwinSlider : MonoBehaviour {


	[SerializeField] private Slider _sliderOne;
	[SerializeField] private Slider _sliderTwo;

	[SerializeField] private Image _background;
	[SerializeField] private Image _filler;

	[SerializeField] private Color _color;

    [SerializeField] private int _min = 0;
	[SerializeField] private int _max = 1;

	[SerializeField] private TMPro.TextMeshProUGUI _minPersonText;
	[SerializeField] private TMPro.TextMeshProUGUI _maxPersonText;

	[SerializeField] private string _addTextToMin;
	[SerializeField] private string _addTextToMax;

	private int _minValue;
	private int _maxValue;

	private RectTransform _fillerRect;
	private float _width;

	public Action<int, int> OnSliderChange;

	private void Awake () {
		_fillerRect = _filler.GetComponent<RectTransform> ();

		_width = GetComponent<RectTransform> ().rect.width / (float)(_max - _min);

		SetSliderSettings(_sliderOne, _min);
		SetSliderSettings(_sliderTwo, _max);

		_minValue = _min;
		_maxValue = _max;

		_filler.color = _color;
	}

	private void SetSliderSettings(Slider slider, int startValue)
	{
		slider.minValue = _min;
		slider.maxValue = _max;
		slider.value = startValue;

		slider.onValueChanged.AddListener(OnCorrectSlider);
	}

	public void OnCorrectSlider (float value) {

		if(_sliderOne.value < _sliderTwo.value)
		{
			_minValue = (int)_sliderOne.value;
            _maxValue = (int)_sliderTwo.value;
        }
		else
		{
            _minValue = (int)_sliderTwo.value;
            _maxValue = (int)_sliderOne.value;
        }

		_minPersonText.text = _addTextToMin + " " + _minValue;
		_maxPersonText.text = _addTextToMax + " " + _maxValue;

        DrawFiller(_minValue, _maxValue);
        OnSliderChange?.Invoke(_minValue, _maxValue);
    }

	void DrawFiller (float min, float max) {


		_fillerRect.offsetMin = new Vector2 ((min - _min)  * _width, 0f);
		_fillerRect.offsetMax = new Vector2 (-(_max - max)  * _width, 0f);
	}

}