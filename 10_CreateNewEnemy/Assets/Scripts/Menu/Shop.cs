using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] private List<Weapon> _weapons;
	[SerializeField] private Player _player;
	[SerializeField] private WeaponViewInShop _shopTemplate;
	[SerializeField] private WeaponViewInPlayer _playerTemplate;
	[SerializeField] private GameObject _itemShopContainer;
	[SerializeField] private GameObject _itemPlayerContainer;
	[SerializeField] private PlayerSetWeapon _PlayerPanel;

	private void Start()
	{
		for (int i = 0; i < _weapons.Count; i++)
		{
			foreach (var currentBarrel in _player.CurrentTwoBarrels)
			{
				if (currentBarrel == _weapons[i])
				{
					_weapons[i].ApplyIsBuyed(true);
					break;
				}
			}

			AddItemInShop(_weapons[i]);
		}
	}

	private void AddItemInShop(Weapon weapon)
	{
		var view = Instantiate(_shopTemplate, _itemShopContainer.transform);

		view.SellButtonClick += OnSellButtonClick;

		view.Render(weapon);

		view.TryLockItem();
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

				Debug.Log("Sell is Done.");

				_PlayerPanel.RefreshItemInPlayer();
			}

			view.SellButtonClick -= OnSellButtonClick;
		}
	}
}
