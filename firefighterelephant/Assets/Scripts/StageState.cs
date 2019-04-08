using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageState : MonoBehaviour
{
	public int levelIndex;

	[SerializeField] private TextMeshProUGUI levelLabel;

	[SerializeField] private Image[] starScore;

	private void Awake()
	{
		levelLabel.text = levelIndex.ToString("00");
	}

	private void Start()
	{
		SetupStars();
	}

	private void SetupStars()
	{
		int levelScore = PlayerPrefsManager.Instance.GetLevelScore(levelIndex);
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
}
