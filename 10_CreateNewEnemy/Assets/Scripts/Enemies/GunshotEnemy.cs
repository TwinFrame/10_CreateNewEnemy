using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunshotEnemy : Enemy
{
	[SerializeField] Bullet _bullet;
	[SerializeField] Transform _shootPosition;

	public override void Hit()
	{
		Instantiate(_bullet, _shootPosition.position, Quaternion.identity, transform);



		//тут нужно переводить в MoveState 
	}
}
