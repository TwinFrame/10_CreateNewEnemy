using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
	[SerializeField] protected TMP_Text Label;
	[SerializeField] protected Image Icon;
	[SerializeField] protected TMP_Text Price;
	[SerializeField] protected TMP_Text Damage;
	[SerializeField] protected TMP_Text Speed;
	[SerializeField] protected Image Background;

	[SerializeField] protected Color MainWeaponColor;
	[SerializeField] protected Color BonusWeaponColor;

	protected Weapon Weapon;

	public void Render(Weapon weapon)
	{
		Weapon = weapon;

		Label.text = Weapon.Label;
		Icon.sprite = Weapon.GetComponent<SpriteRenderer>().sprite;
		Price.text = Weapon.Price.ToString();
		Damage.text = Weapon.BulletThisWeapon.Damage.ToString();
		Speed.text = Weapon.BulletThisWeapon.Speed.ToString();

		if (Weapon.MainBarrel)
		{
			Background.color = MainWeaponColor;
		}
		else
		{
			Background.color = BonusWeaponColor;
		}

		//TryLockItem();
	}
}
