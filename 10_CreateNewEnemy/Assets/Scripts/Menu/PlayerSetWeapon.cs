using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetWeapon : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private WeaponViewInPlayer _playerTemplate;
	[SerializeField] private GameObject _weaponsPlayerContainer;
	[SerializeField] private GameObject _mainWeaponContainer;
	[SerializeField] private GameObject _bonusWeaponContainer;

	private void Start()
	{
		foreach (var currentBarrel in _player.CurrentTwoBarrels)
		{
			currentBarrel.ApplyCurrentWeapon(true);
		}

		for (int i = 0; i < _player.Barrels.Count; i++)
		{
			AddItemInPlayer(_player.Barrels[i], _weaponsPlayerContainer);
		}

		AddItemInPlayer(_player.CurrentTwoBarrels[0], _mainWeaponContainer);
		AddItemInPlayer(_player.CurrentTwoBarrels[1], _bonusWeaponContainer);
	}

	private void AddItemInPlayer(Weapon weapon, GameObject container)
	{
		var view = Instantiate(_playerTemplate, container.transform);

		view.Render(weapon);

		view.TryLockItem();

		view.SetButtonClick += OnSetButtonClick;
	}

	public void RefreshItemInPlayer()
	{
		for (int i = 0; i < _weaponsPlayerContainer.transform.childCount; i++)
		{
			Destroy(_weaponsPlayerContainer.transform.GetChild(i).gameObject);
		}
		for (int i = 0; i < _player.Barrels.Count; i++)
			AddItemInPlayer(_player.Barrels[i], _weaponsPlayerContainer);


		for (int i = 0; i < _mainWeaponContainer.transform.childCount; i++)
		{
			Destroy(_mainWeaponContainer.transform.GetChild(i).gameObject);
		}
		AddItemInPlayer(_player.CurrentTwoBarrels[0], _mainWeaponContainer);


		for (int i = 0; i < _bonusWeaponContainer.transform.childCount; i++)
		{
			Destroy(_bonusWeaponContainer.transform.GetChild(i).gameObject);
		}
		AddItemInPlayer(_player.CurrentTwoBarrels[1], _bonusWeaponContainer);
	}

	private void OnSetButtonClick(Weapon weapon, WeaponViewInPlayer view)
	{
		TrySetWeapon(weapon, view);
	}

	private void TrySetWeapon(Weapon weapon, WeaponViewInPlayer view)
	{
		if (weapon.TryGetComponent<Barrel>(out Barrel barrel))
		{
			if (_player.SetCurrentWeapon(barrel))
			{
				RefreshItemInPlayer();

				Debug.Log("Set is Done.");
			}

			view.SetButtonClick -= OnSetButtonClick;
		}
	}
}
