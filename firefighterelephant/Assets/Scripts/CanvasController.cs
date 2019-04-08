using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
	public TextMeshProUGUI TimerText;
	public TextMeshProUGUI CiviliansSavedText;
	public TextMeshProUGUI FiresExtinguishedText;

	private void OnEnable()
	{
		GameController.UpdateTimer += UpdateTimer;
		GameController.SaveCivilian += UpdateCiviliansSaved;
		GameController.ExtinguishFire += UpdateFiresExtinguished;
	}

	private void OnDisable()
	{
		GameController.UpdateTimer -= UpdateTimer;
		GameController.SaveCivilian -= UpdateCiviliansSaved;
		GameController.ExtinguishFire -= UpdateFiresExtinguished;
	}

	private void Start()
	{
		UpdateCiviliansSaved();
		UpdateFiresExtinguished();
	}

	private void UpdateTimer()
	{
		var result = TimeSpan.FromSeconds(GameController.ActualTime);
		TimerText.text = result.ToString("mm':'ss");
	}

	private void UpdateCiviliansSaved()
	{
		CiviliansSavedText.text = GameController.CiviliansSaved + "/" + GameController.CiviliansToSave;
	}

	private void UpdateFiresExtinguished()
	{
		FiresExtinguishedText.text = GameController.FiresExtinguished + "/" + GameController.FiresToExtinguish;
	}
}
