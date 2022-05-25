using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleKeyManager : MonoBehaviour
{

    public GameObject[] engagedKey;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < engagedKey.Length; i++)
        {
            bool isUsed = PlayerPrefs.GetInt("usedKey" + (i+1), 0) == 1;
            engagedKey[i].SetActive(isUsed);
               
        }    
        
    }

    public void UseKey(int keyNumber)
    {
        Debug.Log("CKM using Key");
        //PlayerPrefs.SetInt("usedKey" + keyNumber, 1);
        PlayerPrefs.SetInt("usedKey" + keyNumber, 1);
        PlayerPrefs.SetInt("holdKey" + keyNumber, 0);
        engagedKey[(keyNumber - 1)].SetActive(true);
    }
}
