using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class HummerEnemy : Enemy
{
	[SerializeField] private int _damage;
	[SerializeField] private Transform _hammer;
	[SerializeField] private float _startZrotate;
	[SerializeField] private float _finishZrotate;

	private bool _isKicking;
	private float _kickingTime = 0.25f;
	private float _currentTime;

	private Coroutine _hummerKickingJob;

	public override void Hit()
	{

		if (!_isKicking && _hummerKickingJob != null)
			StopCoroutine(HummerKicking());

		if (!_isKicking)
			_hummerKickingJob = StartCoroutine(HummerKicking());
	}

	private IEnumerator HummerKicking()
	{
		_isKicking = true;

		while (_currentTime <= _kickingTime)
		{
			var time = _currentTime / _kickingTime;

			 _currentTime += Time.deltaTime;

			_hammer.rotation = new Quaternion(_hammer.rotation.x, _hammer.rotation.y, Mathf.Lerp(Mathf.Deg2Rad * _startZrotate, Mathf.Deg2Rad * _finishZrotate, time), transform.rotation.w);
			
			yield return null;
		}

		_currentTime = 0;

		while (_currentTime <= _kickingTime * 3f)
		{
			var time = _currentTime / (_kickingTime * 3f);

			_currentTime += Time.deltaTime;

			_hammer.rotation = new Quaternion(_hammer.rotation.x, _hammer.rotation.y, Mathf.Lerp(Mathf.Deg2Rad * _finishZrotate, Mathf.Deg2Rad * _startZrotate, time), transform.rotation.w);
			
			yield return null;
		}

		_currentTime = 0;

		_isKicking = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out Player player))
		{
			player.ApplyDamage(_damage);
		}
	}
}
