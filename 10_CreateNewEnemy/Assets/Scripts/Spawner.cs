using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
	[SerializeField] private List<Wave> _waves;
	[SerializeField] private Transform _spawnPoint;
	[SerializeField] private Player _player;

	private Wave _currentWave;
	private int _currentWaveNumber = 0;
	private int _currentNumberEnemy;
	private float _timeAfterLastSpawn;
	private int _spawnedEnemy;

	public event UnityAction AllEnemySpawned;

	private void Start()
	{
		SetWave(_currentWaveNumber);
	}

	private void Update()
	{
		if (_currentWave == null)
			return;

		_timeAfterLastSpawn += Time.deltaTime;

		if (_timeAfterLastSpawn >= _currentWave.Delay)
		{
			InstantiateEnemy();
			_spawnedEnemy++;
			_timeAfterLastSpawn = 0;
		}

		if (_currentWave.Count <= _spawnedEnemy)
		{
			if (_waves.Count > _currentWaveNumber + 1)
			{
				AllEnemySpawned?.Invoke();
			}

			_currentWave = null;
		}
	}

	public void NextWave()
	{
		SetWave(++_currentWaveNumber);

		_spawnedEnemy = 0;
	}

	private void InstantiateEnemy()
	{
		_currentNumberEnemy = Random.Range(0, _currentWave.Templates.Count);

		Enemy enemy = Instantiate(_currentWave.Templates[_currentNumberEnemy], _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
		enemy.Init(_player);

		enemy.Dying += OnEnemyDying;
	}

	private void SetWave(int index)
	{
		_currentWave = _waves[index];
	}

	private void OnEnemyDying(Enemy enemy)
	{
		enemy.Dying -= OnEnemyDying;

		_player.AddMoney(enemy.Reward);
	}
}

[System.Serializable]
public class Wave
{
	public List<Enemy> Templates;
	public float Delay;
	public int Count;
}