using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class AttackState : State
{
	[SerializeField] private float _delay;

	private float _lastAttackTime;
	private Enemy _enemy;

	private void Start()
	{
		_enemy = GetComponent<Enemy>();
	}

	private void Update()
	{
		if (_lastAttackTime <= 0)
		{
			_enemy.Hit();

			_lastAttackTime = _delay;
		}

		_lastAttackTime -= Time.deltaTime;
	}
}
