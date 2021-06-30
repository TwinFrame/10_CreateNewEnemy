using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponViewInPlayer : WeaponView
{
	[SerializeField] private Button _setWeaponButton;

	public event UnityAction<Weapon, WeaponViewInPlayer> SetButtonClick;

	private void OnEnable()
	{
		_setWeaponButton.onClick.AddListener(OnButtonClick);
		_setWeaponButton.onClick.AddListener(TryLockItem);
	}

	private void OnDisable()
	{
		_setWeaponButton.onClick.RemoveListener(OnButtonClick);
		_setWeaponButton.onClick.RemoveListener(TryLockItem);
	}
	public void TryLockItem()
	{
		if (Weapon.CurrentWeapon)
		{
			_setWeaponButton.gameObject.SetActive(false);
		}
	}

	private void OnButtonClick()
	{
		SetButtonClick?.Invoke(Weapon, this);
	}
}
