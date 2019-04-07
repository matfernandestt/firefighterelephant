using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static event Action UpdateTimer;
	public static float ActualTime = 300f;

	public GameObject ScreenFade;

	private void Start()
	{
		ScreenFade.SetActive(true);

        OnUpdateTimer();
		StartCoroutine(ChangeTimer());
	}

	private IEnumerator ChangeTimer()
	{
		while (ActualTime > 0)
		{
			yield return new WaitForSeconds(1f);

			ActualTime--;

			OnUpdateTimer();
		}
	}

	private static void OnUpdateTimer()
	{
		UpdateTimer?.Invoke();
	}
}
