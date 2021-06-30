using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyViewer : NumberViewer
{
	private void OnEnable()
	{
		_player.MoneyChanged += OnValueChanged;

		_text.text = _player.Money.ToString();
	}

	private void OnDisable()
	{
		_player.MoneyChanged -= OnValueChanged;
	}
}
