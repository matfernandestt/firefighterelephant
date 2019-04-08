using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState { MainMenu, LevelSelect, Credits };

public class MainMenu : MonoBehaviour
{
	[SerializeField] private MenuState menuState = MenuState.MainMenu;
	[Space]
	[SerializeField] private CanvasGroup main;
	[SerializeField] private CanvasGroup levelSelect;
	[SerializeField] private CanvasGroup credits;

	private List<GameObject> GameScreens = new List<GameObject>();
	private List<Button> buttons = new List<Button>();

	private Coroutine currentCoroutine;

	private void Awake()
	{
		main.gameObject.SetActive(true);
		levelSelect.gameObject.SetActive(true);
		credits.gameObject.SetActive(true);
		foreach (Button btn in FindObjectsOfType<Button>())
			buttons.Add(btn);

		GameScreens.Add(main.gameObject);
		GameScreens.Add(levelSelect.gameObject);
		GameScreens.Add(credits.gameObject);

		levelSelect.gameObject.SetActive(false);
		credits.gameObject.SetActive(false);
	}

	private void Start()
	{
		foreach(GameObject screen in GameScreens)
		{
			screen.SetActive(false);
		}
		main.gameObject.SetActive(true);
	}

	public void OpenLevelSelect()
	{
		if (currentCoroutine != null)
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(TransitionFromTo(main, levelSelect, MenuState.LevelSelect));
	}

	public void OpenCredits()
	{
		if (currentCoroutine != null)
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(TransitionFromTo(main, credits, MenuState.Credits));
	}

	public void BackToMainFromCredits()
	{
		if (currentCoroutine != null)
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(TransitionFromTo(credits, main, MenuState.MainMenu));
	}

	public void BackToMainFromLevelSelect()
	{
		if (currentCoroutine != null)
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(TransitionFromTo(levelSelect, main, MenuState.MainMenu));
	}

	IEnumerator TransitionFromTo(CanvasGroup scene1, CanvasGroup scene2, MenuState newState)
	{
		DisableAllButtons();
		float progress = 1;
		while (progress > 0)
		{
			progress -= .05f;
			scene1.alpha = progress;
			yield return new WaitForSeconds(.001f);
		}
		scene1.gameObject.SetActive(false);
		scene1.alpha = 1;
		scene2.alpha = 0;
		scene2.gameObject.SetActive(true);
		while (progress < 1)
		{
			progress += .1f;
			scene2.alpha = progress;
			yield return new WaitForSeconds(.01f);
		}
		menuState = newState;
		EnableAllButtons();
	}

	public void LevelSelected()
	{
		FadeManager.Instance.FadeToScene("SCN_Toy");
	}

	#region Button management
	public void EnableAllButtons()
	{
		foreach (Button btn in buttons)
			btn.interactable = true;
	}

	public void DisableAllButtons()
	{
		foreach (Button btn in buttons)
			btn.interactable = false;
	}
	#endregion
}