using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;

    public void controlsWindow ()
    {
        Debug.Log("controls window");
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
}
