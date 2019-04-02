using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
	public TextMeshProUGUI TimerText;

	private void OnEnable()
	{
		GameController.UpdateTimer += UpdateTimer;
	}

	private void OnDisable()
	{
		GameController.UpdateTimer -= UpdateTimer;
	}

	private void UpdateTimer()
	{
		TimeSpan result = TimeSpan.FromSeconds(GameController.ActualTime);
		TimerText.text = result.ToString("mm':'ss");
	}
}
