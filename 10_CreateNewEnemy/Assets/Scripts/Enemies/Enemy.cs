using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(AudioSource))]

public abstract class Enemy : MonoBehaviour
{
	[SerializeField] private string _label;
	[SerializeField] private int _maximumHealth;
	[SerializeField] private int _reward;

	private Player _target;
	private int _health;
	protected AudioSource _audioSource;

	public event UnityAction<Enemy> Dying;

	public int Reward => _reward;
	public Player Target => _target;

	private void Awake()
	{
		_health = _maximumHealth;
		_audioSource = GetComponent<AudioSource>();
	}

	public abstract void Hit();

	public void Init(Player target)
	{
		_target = target;
	}

	public void ApplyDamage(int damage)
	{
		_health -= damage;

		if (_health <= 0)
		{
			Dying?.Invoke(this);
			Destroy(gameObject);
		}
	}
}
