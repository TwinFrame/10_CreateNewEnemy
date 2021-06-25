using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] private string _label;
	[SerializeField] private int _price;
	[SerializeField] private bool _mainBarrel;

	[SerializeField] protected Bullet Bullet;
	[SerializeField] protected Transform ShootPosition;

	[SerializeField] private bool _isBuyed = false;

	public string Label => _label;
	public int Price => _price;
	public bool IsBuyed => _isBuyed;
	public bool MainBarrel => _mainBarrel;
	//public Sprite Sprite => _sprite;

	public Bullet BulletThisWeapon => Bullet;

	public void Buy()
	{
		_isBuyed = true;
	}

	public void Shoot()
	{
		Instantiate(Bullet, ShootPosition.position, Quaternion.identity, transform.parent);
	}
}
