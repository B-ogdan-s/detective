using System;
using UnityEngine;
using UnityEngine.UI;

public class TwinSlider : MonoBehaviour {


	[SerializeField]
	private Slider _sliderOne;

	[SerializeField]
	private Slider _sliderTwo;

	[SerializeField]
	private Image _background;

	[SerializeField]
	private Image _filler;

	[SerializeField]
	private Color _color;

	public float Min = 0f;

	public float Max = 1f;

	public float Border = 3f;

	private RectTransform _fillerRect;

	private float _width;

	public Action<float, float> OnSliderChange;

	private void Awake () {
		_fillerRect = _filler.GetComponent<RectTransform> ();
		_width = GetComponent<RectTransform> ().sizeDelta.x / 2f;
		_sliderOne.minValue = Min;
		_sliderOne.maxValue = Max;
		_sliderTwo.minValue = Min;
		_sliderTwo.maxValue = Max;
		_filler.color = _color;
		if (OnSliderChange == null) {
			OnSliderChange += delegate { };
		}
	}

	public void OnCorrectSliderOne (float value) {
		DrawFiller (_sliderOne.handleRect.localPosition, _sliderTwo.handleRect.localPosition);
		if (value > _sliderTwo.value - Border) {
			_sliderOne.value = _sliderTwo.value - Border;
		} else {
			OnSliderChange?.Invoke (_sliderOne.value, _sliderTwo.value);
		}
	}

	public void OnCorrectSliderTwo (float value) {
		DrawFiller (_sliderOne.handleRect.localPosition, _sliderTwo.handleRect.localPosition);
		if (value < _sliderOne.value + Border) {
			_sliderTwo.value = _sliderOne.value + Border;
		} else {
			OnSliderChange?.Invoke (_sliderOne.value, _sliderTwo.value);
		}
	}
	void DrawFiller (Vector3 one, Vector3 two) {
		float left = Mathf.Abs (_width + one.x);
		float right = Mathf.Abs (_width - two.x);
		_fillerRect.offsetMax = new Vector2 (-right, 0f);
		_fillerRect.offsetMin = new Vector2 (left, 0f);
	}

}