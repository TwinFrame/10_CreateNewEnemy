using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CelebrationState : State
{
	private float _offsetXPosition = 1f;

	private void OnEnable()
	{
		transform.DOMoveX(transform.position.x + _offsetXPosition, 3f).SetEase(Ease.InOutQuart).SetLoops(-1, LoopType.Yoyo);
	}
}
