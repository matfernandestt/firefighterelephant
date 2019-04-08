using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	[SerializeField] private Animator anim;

	#region Delegates
	private void OnEnable()
	{
		GameController.GameOver += GameOver;
	}

	private void OnDisable()
	{
		GameController.GameOver -= GameOver;
	}
	#endregion

	public void GameOver()
	{
		anim.SetTrigger("MenuOpen");
		Time.timeScale = 0;
	}

	public void ResetLevel()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
