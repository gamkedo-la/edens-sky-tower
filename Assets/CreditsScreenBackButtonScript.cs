using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreenBackButtonScript : MonoBehaviour
{
    [SerializeField] GameObject titleScreenParentGameObject;
    [SerializeField] GameObject creditsScreenParentGameObject;

    public void HandleClick()
    {
        titleScreenParentGameObject.SetActive(true);
        creditsScreenParentGameObject.SetActive(false);
    }
}
