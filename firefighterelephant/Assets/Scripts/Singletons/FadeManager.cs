using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : SingletonOfType<FadeManager>
{
	[SerializeField] private Image fadeImage;

	private Coroutine currentCoroutine;

    public int CurrentLevel;

	public void FadeToScene(string sceneName)
	{
		if (currentCoroutine != null)
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(FadeInAndOutToScene(sceneName));
	}

	private IEnumerator FadeInAndOutToScene(string sceneName)
	{
		float progress = 0;
		while(progress < 1)
		{
			progress += .1f;
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, progress);
			yield return new WaitForSeconds(.01f);
		}
		SceneManager.LoadScene(sceneName);
		while (progress > 0)
		{
			progress -= .1f;
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, progress);
			yield return new WaitForSeconds(.01f);
		}
	}

	public void FadeToFunction(MonoBehaviour targetScript, string targetFunction)
	{
		if (currentCoroutine != null)
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(FadeInToAndOutToFunction(targetScript, targetFunction));
	}

	private IEnumerator FadeInToAndOutToFunction(MonoBehaviour targetScript, string targetFunction)
	{
		float progress = 0;
		while (progress < 1)
		{
			progress += .1f;
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, progress);
			yield return new WaitForSeconds(.01f);
		}
		targetScript.Invoke(targetFunction, 0);
		while (progress > 0)
		{
			progress -= .1f;
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, progress);
			yield return new WaitForSeconds(.01f);
		}
	}
}
