using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToPuzzle : MonoBehaviour
{
    public string sceneToLoad;
    public static int destroyCollider = 0;

    void Update ()
    {
        if (destroyCollider == 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider other) {
        //destroyCollider = destroyCollider + 1;
        SceneManager.LoadScene(sceneToLoad);
        
    }

}
