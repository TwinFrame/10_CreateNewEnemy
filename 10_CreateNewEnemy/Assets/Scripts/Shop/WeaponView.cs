using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
	[SerializeField] private TMP_Text _label;
	[SerializeField] private Image _icon;
	[SerializeField] private TMP_Text _price;
	[SerializeField] private TMP_Text _damage;
	[SerializeField] private TMP_Text _speed;
	[SerializeField] protected Button _sellButton;
	[SerializeField] private Image _background;

	[SerializeField] private Color _MainWeaponColor;
	[SerializeField] private Color _BonusWeaponColor;

	private Weapon _weapon;

	public event UnityAction<Weapon, WeaponView> SellButtonClick;

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

	private void TryLockItem()
	{
		if (_weapon.IsBuyed)
		{
			_sellButton.interactable = false;
			//_sellButton.gameObject.SetActive(false);
		}
	}
	private void OnButtonClick()
	{
		SellButtonClick?.Invoke(_weapon, this);
	}

	public void Render(Weapon weapon)
	{
		_weapon = weapon;

		_label.text = _weapon.Label;
		_icon.sprite = _weapon.GetComponent<SpriteRenderer>().sprite;
		_price.text = _weapon.Price.ToString();
		_damage.text = _weapon.BulletThisWeapon.Damage.ToString();
		_speed.text = _weapon.BulletThisWeapon.Speed.ToString();

		if (_weapon.MainBarrel)
		{
			_background.color = _MainWeaponColor;
		}
		else
		{
			_background.color = _BonusWeaponColor;
		}

		TryLockItem();
	}
}
