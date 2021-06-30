using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] private List<Weapon> _weapons = new List<Weapon>();
	[SerializeField] private Player _player;
	[SerializeField] private WeaponViewInShop _shopTemplate;
	[SerializeField] private GameObject _itemShopContainer;
	[SerializeField] private GameObject _itemPlayerContainer;

	public void RefreshItemInShop()
	{
		RefreshContainer(_itemShopContainer, _weapons);
		RefreshContainer(_itemPlayerContainer, _player.GetAllWeapons());
	}

	private void RefreshContainer(GameObject container, List<Weapon> weapon)
	{
		for (int i = 0; i < container.transform.childCount; i++)
		{
			Destroy(container.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < weapon.Count; i++)
		{
			AddItem(weapon[i], container);
		}
	}

	private void AddItem(Weapon weapon, GameObject container)
	{
		var view = Instantiate(_shopTemplate, container.transform);

		view.SellButtonClick += OnSellButtonClick;

		view.Render(weapon);

		if (container == _itemShopContainer)
		{
			view.TryLockItem();
		}

		if (container == _itemPlayerContainer)
		{
			view.DisableButton();
		}		
	}

	private void OnSellButtonClick(Weapon weapon, WeaponViewInShop view)
	{
		TrySellWeapon(weapon, view);
	}

	private void TrySellWeapon(Weapon weapon, WeaponViewInShop view)
	{
		if (weapon.Price <= _player.Money)
		{
			if (_player.BuyWeapon(weapon))
			{
				weapon.Buy();

				RefreshItemInShop();

				Debug.Log("Sell is Done.");
			}

			view.SellButtonClick -= OnSellButtonClick;
		}
	}
}
