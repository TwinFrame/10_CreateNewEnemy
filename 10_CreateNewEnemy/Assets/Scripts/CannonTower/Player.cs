using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(AudioSource))]


public class Player : MonoBehaviour
{
	[SerializeField] private int _maximumHealth;
	[SerializeField] private Transform _barrelMainPositionInsideTower;
	[SerializeField] private Transform _barrelStartPositionInsideTower;
	[SerializeField] private List<Barrel> _currentTwoBarrels = new List<Barrel>(2);
	[SerializeField] private AudioClip _changeWeaponsSound;

	private List<Barrel> _unusedBarrels = new List<Barrel>();
	private float _timePullBarrel = 0.25f;
	private float _BonusWeaponTime = 5f;
	private float _currentPullTime;

	private Coroutine _pullInBarrelJob;
	private Coroutine _changingBarrelJob;
	private Coroutine _powerfulWeaponForTimeJob;
	private WaitForSeconds _waitBonusWeaponTime;
	private WaitForSeconds _waitChangeBarrelTime;
	private bool _isPowerfulWeapon;

	private int _health;
	private Barrel _currentBarrel;
	private Barrel _currenMainBarrelAtTimeEnteringMenu;
	private int _currentBarrelNumber;
	private Animator _animator;
	private AudioSource _audioSource;

	public int Money { get; private set; }

	public List<Barrel>  UnusedBarrels => _unusedBarrels;
	public List<Barrel>  CurrentTwoBarrels => _currentTwoBarrels;

	public event UnityAction<int, int> HealthChanged;
	public event UnityAction<int> MoneyChanged;
	public event UnityAction<float> StartBonusWeaponPeriod;

	private void Awake()
	{
		_waitBonusWeaponTime = new WaitForSeconds(_BonusWeaponTime);

		_waitChangeBarrelTime = new WaitForSeconds(_timePullBarrel);

		SetMaximumHealth();

		_animator = GetComponent<Animator>();

		_audioSource = GetComponent<AudioSource>();

		_currentBarrelNumber = 0;
	}

	private void Start()
	{
		MarkCurrentTwoBarrels();

		foreach (var currentBarrel in _currentTwoBarrels)
			currentBarrel.ApplyIsBuyed(true);

		InitiateCurrentBarrel(_currentTwoBarrels[_currentBarrelNumber]);

		_pullInBarrelJob = StartCoroutine(PullInBarrel());

		//Money = 500;

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
			StartingBonusWeaponPeriod();
		}
	}

	private void MarkCurrentTwoBarrels()
	{
		foreach (var currentBarrel in _currentTwoBarrels)
			currentBarrel.ApplyCurrentWeapon(true);
	}

	public void StartingBonusWeaponPeriod()
	{
		if (!_isPowerfulWeapon)
		{
			_powerfulWeaponForTimeJob = StartCoroutine(BonusWeaponPeriod());
		}
	}

	public void SaveCurrentMainBarrelAtTimeEnterMenu()
	{
		_currenMainBarrelAtTimeEnteringMenu = _currentTwoBarrels[0];
	}

	public void TryChangeCurrentMainBarrelAfterExitMenu()
	{
		if (_currenMainBarrelAtTimeEnteringMenu == null)
			return;

		if (_currenMainBarrelAtTimeEnteringMenu != _currentTwoBarrels[0])
		{
			if (_changingBarrelJob != null)
				StopCoroutine(_changingBarrelJob);

			_changingBarrelJob = StartCoroutine(ChangingBarrel(_currentTwoBarrels[0]));
		}
	}

	private void SetMaximumHealth()
	{
		_health = _maximumHealth;
	}

	private void InitiateCurrentBarrel(Barrel barrel)
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

			_unusedBarrels.Add(barrel);

			MoneyChanged?.Invoke(Money);

			return true;
		}
	}

	public List<Weapon> GetAllWeapons()
	{
		List<Weapon> allWeapons = new List<Weapon>();

		foreach (var currentBarrel in _currentTwoBarrels)
		{
			allWeapons.Add(currentBarrel);
		}

		foreach (var barrel in _unusedBarrels)
		{
			allWeapons.Add(barrel);
		}

		return allWeapons;
	}

	public List<Weapon> GetUnusedWeapons()
	{
		List<Weapon> unusedWeapons = new List<Weapon>();

		foreach (var barrel in _unusedBarrels)
		{
			unusedWeapons.Add(barrel);
		}

		return unusedWeapons;
	}

	public bool SetCurrentBarrelFromUnusedBarrel(Barrel barrel)
	{
		Barrel currentMainBarrel = _currentTwoBarrels[0];
		Barrel currentBonusBarrel = _currentTwoBarrels[1];

		if (barrel.MainBarrel)
		{
			_unusedBarrels.Remove(barrel);

			return ChangeCurrentTwoBarrels(barrel, currentBonusBarrel, currentMainBarrel);
		}

		if (!barrel.MainBarrel)
		{
			_unusedBarrels.Remove(barrel);

			return ChangeCurrentTwoBarrels(currentMainBarrel, barrel, currentBonusBarrel);
		}

		return false;
	}

	private bool ChangeCurrentTwoBarrels(Barrel mainBarrel, Barrel bonusBarrel, Barrel removeFromCurrent)
	{
		_currentTwoBarrels.Clear();

		_currentTwoBarrels.Insert(0, mainBarrel);
		_currentTwoBarrels.Insert(1, bonusBarrel);

		if (removeFromCurrent != null)
		{
			removeFromCurrent.ApplyCurrentWeapon(false);
			_unusedBarrels.Add(removeFromCurrent);
		}

		MarkCurrentTwoBarrels();

		return true;
	}

	private IEnumerator BonusWeaponPeriod()
	{
		_isPowerfulWeapon = true;

		if(_changingBarrelJob != null)
			StopCoroutine(_changingBarrelJob);

		_changingBarrelJob = StartCoroutine(ChangingBarrel(_currentTwoBarrels[1]));

		_audioSource.PlayOneShot(_changeWeaponsSound);

		yield return _waitChangeBarrelTime;

		StartBonusWeaponPeriod?.Invoke(_BonusWeaponTime);

		yield return _waitBonusWeaponTime;

		if (_changingBarrelJob != null)
			StopCoroutine(_changingBarrelJob);

		_changingBarrelJob = StartCoroutine(ChangingBarrel(_currentTwoBarrels[0]));

		_audioSource.PlayOneShot(_changeWeaponsSound);

		yield return _waitChangeBarrelTime;

		_isPowerfulWeapon = false;
	}

	private IEnumerator ChangingBarrel(Barrel newBarrel)
	{
		_audioSource.PlayOneShot(_changeWeaponsSound);

		while (_currentPullTime < _timePullBarrel)
		{
			_currentPullTime += Time.deltaTime;

			var currentNormalizeTime = _currentPullTime / _timePullBarrel;

			_currentBarrel.transform.position = Vector3.Lerp(_barrelMainPositionInsideTower.position, _barrelStartPositionInsideTower.position, currentNormalizeTime);
			_currentBarrel.transform.rotation = Quaternion.Lerp(_barrelMainPositionInsideTower.rotation, _barrelStartPositionInsideTower.rotation, currentNormalizeTime);

			yield return null;
		}

		Destroy(_currentBarrel.gameObject);

		InitiateCurrentBarrel(newBarrel);

		_currentPullTime = 0;

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
