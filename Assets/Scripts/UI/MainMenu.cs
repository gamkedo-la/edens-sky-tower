using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void StartGameButton ()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("Options Scene");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits Scene");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
