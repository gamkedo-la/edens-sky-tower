using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreenBackButtonScript : MonoBehaviour
{
    [SerializeField] GameObject titleScreenParentGameObject;
    [SerializeField] GameObject settingsScreenParentGameObject;

    public void HandleClick()
    {
        titleScreenParentGameObject.SetActive(true);
        settingsScreenParentGameObject.SetActive(false);
    }
}
