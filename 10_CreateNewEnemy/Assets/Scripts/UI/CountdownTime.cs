using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountdownTime : NumberViewer
{
	[SerializeField] private Countdown _countdownElement;

	private void OnEnable()
	{
		_countdownElement.CountdownValueChanged += OnClockdownChanged;
	}

	private void OnDisable()
	{
		_countdownElement.CountdownValueChanged -= OnClockdownChanged;
	}
}
