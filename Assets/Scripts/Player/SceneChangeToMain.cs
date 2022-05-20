using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToMain : MonoBehaviour
{

    public SceneChangeToPuzzle SceneChangeToPuzzle;

    void OnTriggerEnter (Collider other) {
        SceneChangeToPuzzle.destroyCollider = 1;
        SceneManager.LoadScene(0);
    }
}
