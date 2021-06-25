using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponViewInPlayer : WeaponView
{
	[SerializeField] private Button _setWeapon;

	public event UnityAction<Weapon, WeaponViewInPlayer> SetButtonClick;

	private void OnEnable()
	{
		_setWeapon.onClick.AddListener(OnButtonClick);
		_setWeapon.onClick.AddListener(TryLockItem);
	}

	private void OnDisable()
	{
		_setWeapon.onClick.RemoveListener(OnButtonClick);
		_setWeapon.onClick.RemoveListener(TryLockItem);
	}
	public void TryLockItem()
	{
		if (Weapon.CurrentWeapon)
		{
			_setWeapon.gameObject.SetActive(false);
		}
	}

	private void OnButtonClick()
	{
		SetButtonClick?.Invoke(Weapon, this);
	}
}
