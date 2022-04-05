using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonScript : MonoBehaviour
{
    [SerializeField] GameObject titleScreenParentGameObject;
    [SerializeField] GameObject creditsScreenParentGameObject;
    
    public void HandleClick()
    {
        creditsScreenParentGameObject.SetActive(true);
        titleScreenParentGameObject.SetActive(false);
    }
}
