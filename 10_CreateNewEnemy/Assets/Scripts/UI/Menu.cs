using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Menu : MonoBehaviour
{
	[SerializeField] Player _player;

	public void OpenPanel(GameObject panel)
	{
		panel.SetActive(true);
		Time.timeScale = 0;
	}

	public void ClosePanel(GameObject panel)
	{
		panel.SetActive(false);
		Time.timeScale = 1f;
	}

	public void OpenElement(GameObject element)
	{
		element.SetActive(true);
	}

	public void CloseElement(GameObject element)
	{
		element.SetActive(false);
	}

	public void TemporaryMessage(string message, Text text)
	{
		Sequence temporaryMessage = DOTween.Sequence();

		temporaryMessage.Append(text.DOText(message, 1f, true)).SetRelative();
		temporaryMessage.PrependInterval(1f).SetRelative();
		temporaryMessage.Append(text.DOText("", 1f, true)).SetRelative();
	}

	public void Exit()
	{
		Application.Quit();
	}
}
