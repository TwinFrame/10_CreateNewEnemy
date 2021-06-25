using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewAtPlayer : WeaponView
{
	[SerializeField] private Button _setWeapon;

	private void Start()
	{
		_sellButton.gameObject.SetActive(false);
	}
}
