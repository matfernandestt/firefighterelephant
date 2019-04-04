using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    #region Delegates
    private void OnEnable()
    {
        PlayerInput.PauseButtonDown += PauseMenu;
    }

    private void OnDisable()
    {
        PlayerInput.PauseButtonDown -= PauseMenu;
    }
    #endregion

    public void PauseMenu()
    {
        switch (Time.timeScale)
        {
            case 0:
                anim.SetTrigger("MenuClose");
                Time.timeScale = 1;
                break;
            case 1:
                anim.SetTrigger("MenuOpen");
                Time.timeScale = 0;
                break;
        }
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
