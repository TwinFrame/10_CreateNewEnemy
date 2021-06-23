using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIsland : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private Transform _pathPoints;

	private Transform[] _points;
	private int _currentPoint;
	private Coroutine _walkToPathJob;

	private void Awake()
	{
		_points = new Transform[_pathPoints.childCount];

		for (int i = 0; i < _pathPoints.childCount; i++)
		{
			_points[i] = _pathPoints.GetChild(i);
		}
	}

	private void Start()
	{
		_walkToPathJob = StartCoroutine(WalkToPath());
	}

	private IEnumerator WalkToPath()
	{
		while (true)
		{
			transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].transform.position, _speed * Time.deltaTime);

			if (transform.position == _points[_currentPoint].transform.position)
			{
				_currentPoint++;

				if (_currentPoint >= _points.Length)
				{
					_currentPoint = 0;
				}
			}
			yield return null;
		}
	}
}
