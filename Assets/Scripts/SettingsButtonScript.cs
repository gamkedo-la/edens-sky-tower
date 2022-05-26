using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonScript : MonoBehaviour
{
    [SerializeField] GameObject titleScreenParentGameObject;
    [SerializeField] GameObject settingsScreenParentGameObject;

    public void HandleClick()
    {
        settingsScreenParentGameObject.SetActive(true);
        titleScreenParentGameObject.SetActive(false);
    }
}
