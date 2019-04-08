using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
	[SerializeField] private Image[] starScore;

	public int score = 3;

	private void Start()
	{
		SetupStars(score);
	}

	private void SetupStars(int levelScore)
	{
		switch (levelScore)
		{
			case 0:
				foreach (Image star in starScore)
					star.enabled = false;
				break;
			case 1:
				foreach (Image star in starScore)
					star.enabled = false;
				starScore[0].enabled = true;
				break;
			case 2:
				foreach (Image star in starScore)
					star.enabled = false;
				starScore[0].enabled = true;
				starScore[1].enabled = true;
				break;
			case 3:
				foreach (Image star in starScore)
					star.enabled = true;
				break;
			default:
				foreach (Image star in starScore)
					star.enabled = false;
				break;
		}
	}

	public void RestartLevel()
	{

	}

	public void ReturnToMenu()
	{

	}

	public void NextLevel()
	{
		FadeManager.Instance.FadeToScene("SCN_Level" + (GameController.ActualLevel + 1).ToString("00"));
	}
}
