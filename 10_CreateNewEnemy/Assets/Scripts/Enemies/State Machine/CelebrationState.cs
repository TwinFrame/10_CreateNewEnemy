using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CelebrationState : State
{
	private float _offsetXPosition = 2f;

	private void OnEnable()
	{
		transform.DOMoveX(transform.position.x + _offsetXPosition, Random.Range(2f, 5f)).SetEase(Ease.InOutQuart).SetLoops(-1, LoopType.Yoyo);
	}
}
