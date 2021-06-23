using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] private string _label;
	[SerializeField] private int price;
	[SerializeField] private Sprite _icon;

	[SerializeField] protected Bullet Bullet;
	[SerializeField] protected Transform ShootPosition;

	private bool _isBuyed = false;

	public void Shoot()
	{
		Instantiate(Bullet, ShootPosition.position, Quaternion.identity, transform.parent);
	}
}
