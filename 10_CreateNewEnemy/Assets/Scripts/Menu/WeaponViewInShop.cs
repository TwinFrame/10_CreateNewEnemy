using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponViewInShop : WeaponView
{
	[SerializeField] private Button _sellButton;

	public event UnityAction<Weapon, WeaponViewInShop> SellButtonClick;

	private void OnEnable()
	{
		_sellButton.onClick.AddListener(OnButtonClick);
		_sellButton.onClick.AddListener(TryLockItem);
	}

	private void OnDisable()
	{
		_sellButton.onClick.RemoveListener(OnButtonClick);
		_sellButton.onClick.RemoveListener(TryLockItem);
	}

	public void TryLockItem()
	{
		if (Weapon.IsBuyed)
		{
			_sellButton.interactable = false;
		}
	}

	public void DisableButton()
	{
		_sellButton.gameObject.SetActive(false);
	}

	private void OnButtonClick()
	{
		SellButtonClick?.Invoke(Weapon, this);
	}
}
