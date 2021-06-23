using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HummerEnemy : Enemy
{
	[SerializeField] private int _damage;
	[SerializeField] private Transform _hammer;

	private bool _isKicking;
	private float _speed = 1f;
	private Quaternion _startHummerAngle;
	private Quaternion _endHummerAngle = new Quaternion(0, 0, -45f, 1);
	private float _currentTime;

	private Coroutine _hummerKickingJob;

	private void OnEnable()
	{
		_startHummerAngle = _hammer.rotation;
	}

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

		while (_currentTime <= 1f)
		{
			 _currentTime += Time.deltaTime;
			_hammer.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Mathf.Lerp(15f, 0.001f, _currentTime), transform.rotation.w);
			yield return null;
		}

		_currentTime = 0;
		_isKicking = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out Player player))
		{
			player.TakeDamage(_damage);
		}
	}
}
