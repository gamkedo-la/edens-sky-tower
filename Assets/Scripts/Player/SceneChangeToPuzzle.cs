using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToPuzzle : MonoBehaviour
{
    public string sceneToLoad;

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

        if(PlayerPrefsObeliskKey() == PlayerPrefs.GetString("LastUsedPortalWentTo", "original")) {
            Player.instance.transform.position = transform.position - transform.right * 3.0f; // away from red arrow (.right)
            Player.instance.transform.LookAt(transform.position);
            PlayerPrefs.SetString("LastUsedPortalWentTo", "original"); // forget the last portal used
        }
    }

    void OnTriggerEnter (Collider other) {
        bool isSolved = PlayerPrefs.GetInt(PlayerPrefsObeliskKey(), 0) == 1;
        if(isSolved) {
            Debug.Log("already used");
            return;
        }

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
            Debug.Log("normal scene load");
            PlayerPrefs.SetInt(PlayerPrefsObeliskKey(), 1);

            // next variable is used to match when we return
            PlayerPrefs.SetString("LastUsedPortalWentTo", PlayerPrefsObeliskKey());
            SceneManager.LoadScene(sceneToLoad);
        }        
    }

    string PlayerPrefsObeliskKey() {
        return "ObeliskStateOf" + transform.parent.name;
    }

}
