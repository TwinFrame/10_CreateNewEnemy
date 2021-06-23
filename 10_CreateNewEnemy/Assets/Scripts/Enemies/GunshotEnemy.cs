using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunshotEnemy : Enemy
{
	[SerializeField] Bullet _bullet;
	[SerializeField] Transform _shootPosition;

	private float _speedKickbackAfterHit = 10f;
	private Vector3 _distanceKickbackAfterHit = Vector3.left * 2f;

	public override void Hit()
	{
		Instantiate(_bullet, _shootPosition.position, Quaternion.identity, transform);



		//тут нужно переводить в MoveState 
	}

	private IEnumerator KickBackAfterHit()
	{
		while (_speedKickbackAfterHit > 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, transform.position + _distanceKickbackAfterHit, _speedKickbackAfterHit * Time.deltaTime);
			yield return null;
		}
	}
}
