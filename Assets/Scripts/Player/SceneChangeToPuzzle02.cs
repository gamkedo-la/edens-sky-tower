using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToPuzzle02 : MonoBehaviour
{
    void OnTriggerEnter (Collider other) {
        SceneManager.LoadScene(2);
    }
}
