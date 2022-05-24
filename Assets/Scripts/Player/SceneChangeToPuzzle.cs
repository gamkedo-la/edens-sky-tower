using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToPuzzle : MonoBehaviour
{
    public string sceneToLoad;
    public static int destroyCollider = 0;

    public GameObject unsolvedForm;
    public GameObject solvedForm;

    private void Start() {
        if (unsolvedForm == null || solvedForm == null) {
            Debug.Log(gameObject.name + " is missing its solved/unsolved form, replace with prefab");
            return;
        }
        // NOTE: RELIES ON THE PARENT (P_Obelisk) being a unique name per each one
        bool isSolved = PlayerPrefs.GetInt(PlayerPrefsObeliskKey(), 0) == 1;
        unsolvedForm.SetActive(isSolved == false);
        solvedForm.SetActive(isSolved);
    }

    void Update ()
    {
        if (destroyCollider == 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider other) {
        int storyCount = 0;
        for(int i=1;i<=7;i++) {
            if (PlayerPrefs.GetInt("seenTablet"+i, 0) == 1) {
                storyCount++;
            }
        }
        if (storyCount >= 6)
        {
            Debug.Log("scene 4 loaded - all story bits"); 
            SceneManager.LoadScene(sceneToLoad);
        } else
        {
            //destroyCollider = destroyCollider + 1;
            Debug.Log("normal scene load");
            PlayerPrefs.SetInt(PlayerPrefsObeliskKey(), 1);
            SceneManager.LoadScene(sceneToLoad);
        }        
    }

    string PlayerPrefsObeliskKey() {
        return "ObeliskStateOf" + transform.parent.name;
    }

}
