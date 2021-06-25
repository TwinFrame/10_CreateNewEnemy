using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBar : CountBar
{
	[SerializeField] private Player _player;

	private void OnEnable()
	{
		_player.MoneyChanged += OnValueChanged;
	}

	private void OnDisable()
	{
		_player.MoneyChanged -= OnValueChanged;
	}
}
