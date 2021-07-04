using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] private string _label;
	[SerializeField] private int _price;
	[SerializeField] private bool _mainBarrel;
	[SerializeField] protected Bullet Bullet;
	[SerializeField] protected Transform ShootPosition;
	[SerializeField] private bool _isBuyed = false;
	[SerializeField] private bool _isCurrentWeapon = false;

	public string Label => _label;
	public int Price => _price;
	public bool IsBuyed => _isBuyed;
	public bool MainBarrel => _mainBarrel;

	public bool CurrentWeapon => _isCurrentWeapon;


	public Bullet BulletThisWeapon => Bullet;

	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void Buy()
	{
		_isBuyed = true;
	}

	public void ApplyCurrentWeapon(bool value)
	{
		_isCurrentWeapon = value;
	}

	public void ApplyIsBuyed(bool value)
	{
		_isBuyed = value;
	}

	public void Shoot()
	{
		Instantiate(Bullet, ShootPosition.position, Quaternion.identity, transform.parent);

		_audioSource.PlayOneShot(Bullet.ShootSound);
	}
}
