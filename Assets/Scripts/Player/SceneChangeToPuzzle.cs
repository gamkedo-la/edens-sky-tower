using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToPuzzle : MonoBehaviour
{
    public string sceneToLoad;
    public static int destroyCollider = 0;
    public InventoryUI sotryTabletUICount;
    public float puzzleSceneCounter = 0; // deferentiator to checking that its puzzle scene number 4

    void Update ()
    {
        if (destroyCollider == 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider other) {

        if(puzzleSceneCounter != 0)
        {
            if(sotryTabletUICount.StoryCount == 6)
            {
                Debug.Log("scene 4 loaded - all story bits");
                SceneManager.LoadScene(sceneToLoad);
            }
        } else
        {
            //destroyCollider = destroyCollider + 1;
            Debug.Log("normal scene load");
            SceneManager.LoadScene(sceneToLoad);
        }        
    }

}
