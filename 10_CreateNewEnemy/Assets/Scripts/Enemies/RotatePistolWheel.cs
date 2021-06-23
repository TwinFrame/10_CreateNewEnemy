using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePistolWheel : MonoBehaviour
{
	private float _speed = 60f;
	private bool _isRotation = true;

	private void Start()
	{
		StartCoroutine(RotationWheel(_speed));
	}

	private IEnumerator RotationWheel(float speed)
	{
		while (_isRotation)
		{
			transform.Rotate(Vector3.back, _speed * Time.deltaTime);

			yield return null;
		}
	}
}
