using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
	[SerializeField] private Player _player;

	[SerializeField] StartCountdownEvent _startCountdown;
	[SerializeField] StopCountdownEvent _stopCountdown;

	private Coroutine _onCountDownJob;
	private float _currentTime;

	public event UnityAction<float, float> CountdownValueChanged;

	private void OnEnable()
	{
		_player.StartBonusWeaponPeriod += OnCountdown;
	}

	private void OnDisable()
	{
		_player.StartBonusWeaponPeriod -= OnCountdown;
	}

	private void OnCountdown(float time)
	{
		if (_onCountDownJob != null)
			StopCoroutine(_onCountDownJob);

		_onCountDownJob = StartCoroutine(CountdownProgress(time));
	}

	private IEnumerator CountdownProgress(float time)
	{
		_currentTime = 0;

		_startCountdown?.Invoke();

		while (_currentTime <= time)
		{
			var reverseTime = (time - _currentTime);

			CountdownValueChanged?.Invoke(reverseTime, time);

			_currentTime += Time.deltaTime;

			yield return null;
		}

		_stopCountdown?.Invoke();
	}
}

[System.Serializable]
public class StartCountdownEvent : UnityEvent { }

[System.Serializable]
public class StopCountdownEvent : UnityEvent { }