using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class NumberViewer : MonoBehaviour
{
	[SerializeField] protected Player _player;
	[SerializeField] protected TMP_Text _text;

	public void OnValueChanged(int value)
	{
		_text.text = value.ToString();
	}

	public void OnValueChanged(float value)
	{
		_text.text = Mathf.RoundToInt(value).ToString();
	}

	public void OnClockdownChanged(float reverseTime, float time)
	{
		_text.text = Mathf.CeilToInt(reverseTime).ToString();
	}
}
