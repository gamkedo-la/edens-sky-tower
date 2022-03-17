using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleKeyManager : MonoBehaviour
{
    public string keyNumber;
    public GameObject engagedKey;

    // Start is called before the first frame update
    void Start()
    {
        bool isUsed = PlayerPrefs.GetInt("usedKey" + keyNumber, 0) == 1;
        if(engagedKey) {
            engagedKey.SetActive (isUsed);
        } else {
            Debug.Log("engagedKey not set up for " + gameObject.name);
        }
        
    }

    public void UseKey()
    {
        //PlayerPrefs.SetInt("usedKey" + keyNumber, 1);
        PlayerPrefs.SetInt("usedKey" + keyNumber, 1);
        PlayerPrefs.SetInt("holdKey" + keyNumber, 0);
        engagedKey.SetActive(true);
    }
}
