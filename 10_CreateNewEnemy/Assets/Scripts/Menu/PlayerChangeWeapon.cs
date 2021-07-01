using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeWeapon : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private WeaponViewInPlayer _playerTemplate;
	[SerializeField] private GameObject _unusedWeaponsContainer;
	[SerializeField] private GameObject _mainWeaponContainer;
	[SerializeField] private GameObject _bonusWeaponContainer;

	public void RefreshItemInPlayer()
	{
		if(_player.GetUnusedWeapons() !=null)
			RefreshContainer(_unusedWeaponsContainer, _player.GetUnusedWeapons());

		RefreshContainer(_mainWeaponContainer, _player.CurrentTwoBarrels[0]);
		RefreshContainer(_bonusWeaponContainer, _player.CurrentTwoBarrels[1]);
	}

	private void RefreshContainer(GameObject container, List<Weapon> weapons)
	{
		for (int i = 0; i < container.transform.childCount; i++)
		{
			Destroy(container.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < weapons.Count; i++)
			AddItemInPlayer(weapons[i], container);
	}

	private void RefreshContainer(GameObject container, Weapon weapon)
	{
		for (int i = 0; i < container.transform.childCount; i++)
		{
			Destroy(container.transform.GetChild(i).gameObject);
		}

		AddItemInPlayer(weapon, container);
	}

	private void AddItemInPlayer(Weapon weapon, GameObject container)
	{
		var view = Instantiate(_playerTemplate, container.transform);

		view.SetButtonClick += OnSetButtonClick;

		view.Render(weapon);

		view.TryLockItem();
	}

	private void OnSetButtonClick(Weapon weapon, WeaponViewInPlayer view)
	{
		TrySetWeapon(weapon, view);
	}

	private void TrySetWeapon(Weapon weapon, WeaponViewInPlayer view)
	{
		if (weapon.TryGetComponent<Barrel>(out Barrel barrel))
		{
			if (_player.SetCurrentBarrelFromUnusedBarrel(barrel))
			{
				RefreshItemInPlayer();
			}

			view.SetButtonClick -= OnSetButtonClick;
		}
	}
}
