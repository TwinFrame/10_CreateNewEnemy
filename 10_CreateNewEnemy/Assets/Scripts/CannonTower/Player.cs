using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]

public class Player : MonoBehaviour
{
	[SerializeField] private int _maximumHealth;
	[SerializeField] private List<Barrel> _currentTwobarrels;
	[SerializeField] private Transform _barrelMainPositionInsideTower;
	[SerializeField] private Transform _barrelStartPositionInsideTower;

	private List<Barrel> _barrels = new List<Barrel>();
	private float _timePullBarrel = 0.25f;
	private float _currentPullTime;

	private Coroutine _pullInBarrelJob;
	private Coroutine _changingBarrelJob;
	private Coroutine _powerfulWeaponForTimeJob;
	private WaitForSeconds _timeOfPowerfulWeapon;
	private WaitForSeconds _timeForEndingPowerfulWeapon;
	private bool _isPowerfulWeapon;

	private int _health;
	private Barrel _currentBarrel;
	private int _currentNumBarrelFromList;
	private Animator _animator;

	public int Money { get; private set; }
	public List<Barrel>  Barrels => _barrels;

	public event UnityAction<int, int> HealthChanged;
	public event UnityAction<int> MoneyChanged;

	private void Awake()
	{
		_timeOfPowerfulWeapon = new WaitForSeconds(5f);

		_timeForEndingPowerfulWeapon = new WaitForSeconds(_timePullBarrel);

		SetMaximumHealth();

		_animator = GetComponent<Animator>();

		_currentNumBarrelFromList = 0;
	}

	private void Start()
	{
		InitiateBarrel(_currentTwobarrels[_currentNumBarrelFromList]);

		_pullInBarrelJob = StartCoroutine(PullInBarrel());

		MoneyChanged?.Invoke(Money);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_currentBarrel.Shoot();
		}

		if (Input.GetMouseButtonDown(1))
		{
			if (!_isPowerfulWeapon)
			{
				_powerfulWeaponForTimeJob = StartCoroutine(PowerfulWeaponForTime());
			}
		}
	}

	private void SetMaximumHealth()
	{
		_health = _maximumHealth;
	}

	private void InitiateBarrel(Barrel barrel)
	{
		_currentBarrel = Instantiate(barrel, _barrelStartPositionInsideTower.position, Quaternion.identity, transform);
	}

	public void ApplyDamage(int damage)
	{
		_health -= damage;

		HealthChanged?.Invoke(_health, _maximumHealth);

		if (_health <= 0)
		{
			Debug.Log("Player is Die.");

			Destroy(gameObject);
		}
	}

	public void AddMoney(int money)
	{
		Money += money;

		MoneyChanged?.Invoke(Money);
	}

	public bool BuyWeapon(Weapon weapon)
	{
		if (!weapon.TryGetComponent<Barrel>(out Barrel barrel))
		{
			return false;
		}
		else
		{
			Money -= weapon.Price;

			_barrels.Add(barrel);

			MoneyChanged?.Invoke(Money);

			return true;
		}
	}

	private IEnumerator PowerfulWeaponForTime()
	{
		_isPowerfulWeapon = true;

		_changingBarrelJob = StartCoroutine(ChangingBarrel());

		yield return _timeOfPowerfulWeapon;

		if (_changingBarrelJob != null)
		{
			StopCoroutine(_changingBarrelJob);
		}

		_changingBarrelJob = StartCoroutine(ChangingBarrel());

		yield return _timeForEndingPowerfulWeapon;

		_isPowerfulWeapon = false;
	}

	private IEnumerator ChangingBarrel()
	{
		while (_currentPullTime < _timePullBarrel)
		{
			_currentPullTime += Time.deltaTime;

			var currentNormalizeTime = _currentPullTime / _timePullBarrel;

			_currentBarrel.transform.position = Vector3.Lerp(_barrelMainPositionInsideTower.position, _barrelStartPositionInsideTower.position, currentNormalizeTime);
			_currentBarrel.transform.rotation = Quaternion.Lerp(_barrelMainPositionInsideTower.rotation, _barrelStartPositionInsideTower.rotation, currentNormalizeTime);

			yield return null;
		}

		Destroy(_currentBarrel.gameObject);

		_currentPullTime = 0;

		if (_currentNumBarrelFromList == 0)
		{
			_currentNumBarrelFromList = 1;
			InitiateBarrel(_currentTwobarrels[_currentNumBarrelFromList]);
		}
		else
		{
			_currentNumBarrelFromList = 0;
			InitiateBarrel(_currentTwobarrels[_currentNumBarrelFromList]);
		}

		while (_currentPullTime < _timePullBarrel)
		{
			_currentPullTime += Time.deltaTime;

			var currentNormalizeTime = _currentPullTime / _timePullBarrel;

			_currentBarrel.transform.position = Vector3.Lerp(_barrelStartPositionInsideTower.position, _barrelMainPositionInsideTower.position, currentNormalizeTime);
			_currentBarrel.transform.rotation = Quaternion.Lerp(_barrelStartPositionInsideTower.rotation, _barrelMainPositionInsideTower.rotation, currentNormalizeTime);

			yield return null;
		}

		_currentPullTime = 0;
	}

	private IEnumerator PullInBarrel()
	{
		while (_currentPullTime < _timePullBarrel)
		{
			_currentPullTime += Time.deltaTime;

			var currentNormalizeTime = _currentPullTime / _timePullBarrel;

			_currentBarrel.transform.position = Vector3.Lerp(_barrelStartPositionInsideTower.position, _barrelMainPositionInsideTower.position, currentNormalizeTime);
			_currentBarrel.transform.rotation = Quaternion.Lerp(_barrelStartPositionInsideTower.rotation, _barrelMainPositionInsideTower.rotation, currentNormalizeTime);

			yield return null;
		}

		_currentPullTime = 0;
	}
}
