using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownBar : Bar
{
	[SerializeField] private Countdown _countdownElement;

	private void OnEnable()
	{
		_countdownElement.CountdownValueChanged += OnValueChanged;

		Slider.value = 1f;
	}

	private void OnDisable()
	{
		_countdownElement.CountdownValueChanged -= OnValueChanged;
	}
}
