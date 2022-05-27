using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWindowFromPauseBackButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;

    public void BackButton()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }
}
