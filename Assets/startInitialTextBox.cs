using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startInitialTextBox : MonoBehaviour
{
    public GameObject textCollider;

    public void StartInitialTeaxtBox()
    {
        textCollider.SetActive(true);
    }
}
