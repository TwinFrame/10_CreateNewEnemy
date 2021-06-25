using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] private List<Weapon> _weapons;
	[SerializeField] private Player _player;
	[SerializeField] private WeaponView _shopTemplate;
	[SerializeField] private WeaponViewAtPlayer _playerTemplate;
	[SerializeField] private GameObject _itemShopContainer;
	[SerializeField] private GameObject _itemPlayerContainer;

	private void Start()
	{
		for (int i = 0; i < _weapons.Count; i++)
		{
			AddItemInShop(_weapons[i]);
		}
	}

	private void AddItemInShop(Weapon weapon)
	{
		var view = Instantiate(_shopTemplate, _itemShopContainer.transform);

		view.SellButtonClick += OnSellButtonClick;

		view.Render(weapon);
	}

	private void AddItemInPlayer(Weapon weapon)
	{
		var view = Instantiate(_shopTemplate, _itemPlayerContainer.transform);

		view.Render(weapon);
	}

	private void RefreshItemInPlayer()
	{
		for (int i = 0; i < _itemPlayerContainer.transform.childCount; i++)
		{
			Destroy(_itemPlayerContainer.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < _player.Barrels.Count; i++)
		{
			AddItemInPlayer(_player.Barrels[i]);
		}
	}

	private void OnSellButtonClick(Weapon weapon, WeaponView view)
	{
		TrySellWeapon(weapon, view);
	}

	private void TrySellWeapon(Weapon weapon, WeaponView view)
	{
		if (weapon.Price <= _player.Money)
		{
			if (_player.BuyWeapon(weapon))
			{
				weapon.Buy();

				Debug.Log("Sell is Done.");

				RefreshItemInPlayer();
			}

			view.SellButtonClick -= OnSellButtonClick;
		}
	}
}
