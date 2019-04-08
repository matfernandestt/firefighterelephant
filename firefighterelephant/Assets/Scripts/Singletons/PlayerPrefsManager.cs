using UnityEngine;

public class PlayerPrefsManager : SingletonOfType<PlayerPrefsManager>
{
	public void SetLevelScore(int index, int starScore)
	{
		PlayerPrefs.SetInt("LEVEL" + index, starScore);
	}

	public int GetLevelScore(int index)
	{
		return PlayerPrefs.GetInt("LEVEL" + index);
	}
}
