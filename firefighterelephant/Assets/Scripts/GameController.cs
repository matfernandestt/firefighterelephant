using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[Header("Properties")]
	public int Level;
	public int Civilians;
	public int Fires;
	public int Time;
	public int UnlockedExtinguishers;

	[Header("Stars")]
	public float Stars3;
	public float Stars2;

	public GameObject EndGameCanvas;

	public static GameObject VictoryScreen;
	public static int CiviliansSaved;
	public static int CiviliansToSave;
	public static int FiresExtinguished;
	public static int FiresToExtinguish;
	public static int Score;

	public static event Action UpdateTimer;
	public static event Action SaveCivilian;
	public static event Action ExtinguishFire;
	public static event Action Victory;
	public static event Action GameOver;
	public static float ActualTime;

	public GameObject ScreenFade;

	private void Start()
	{
		Score = 0;
		CiviliansSaved = 0;
		FiresExtinguished = 0;

		CiviliansToSave = Civilians;
		FiresToExtinguish = Fires;
		ActualTime = Time;

		PlayerFireExtinguisher.UnlockedExtinguishers = UnlockedExtinguishers;

		ScreenFade.SetActive(true);

		VictoryScreen = EndGameCanvas;

		OnUpdateTimer();
		StartCoroutine(ChangeTimer());
	}

	private void OnEnable()
	{
		SaveCivilian += WinningCheck;
		ExtinguishFire += WinningCheck;
	}

	private void OnDisable()
	{
		SaveCivilian -= WinningCheck;
		ExtinguishFire -= WinningCheck;
	}

	private IEnumerator ChangeTimer()
	{
		while (ActualTime > 0)
		{
			yield return new WaitForSeconds(1f);

			ActualTime--;

			OnUpdateTimer();

			if (ActualTime <= 0)
			{
				OnGameOver();
			}
		}
	}

	private void WinningCheck()
	{
		if (CiviliansSaved < CiviliansToSave)
		{
			return;
		}

		if (FiresExtinguished < FiresToExtinguish)
		{
			return;
		}

		if (Time > Stars3)
		{
			Score = 3;
		}
		else if (Time > Stars2)
		{
			Score = 2;
		}
		else
		{
			Score = 1;
		}

		PlayerPrefsManager.Instance.SetLevelScore(Level, Score);

		OnVictory();
	}

	private static void OnUpdateTimer()
	{
		UpdateTimer?.Invoke();
	}

	public static void OnSaveCivilian()
	{
		SaveCivilian?.Invoke();
	}

	public static void OnExtinguishFire()
	{
		ExtinguishFire?.Invoke();
	}

	public static void OnVictory()
	{
		Victory?.Invoke();

		var victoryScreen = Instantiate(VictoryScreen);
		victoryScreen.GetComponent<EndGame>().score = Score;
	}

	public static void OnGameOver()
	{
		GameOver?.Invoke();
	}
}
