using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAndResetGame : MonoBehaviour
{
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("forgetting all player pref - reloding scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
