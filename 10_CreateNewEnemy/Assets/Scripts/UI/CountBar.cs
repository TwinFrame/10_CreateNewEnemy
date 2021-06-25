using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountBar : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;

	public void OnValueChanged(int value)
	{
		_text.text = value.ToString();
	}
}
