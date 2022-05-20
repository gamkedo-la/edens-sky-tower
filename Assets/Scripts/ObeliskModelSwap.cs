using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskModelSwap : MonoBehaviour
{
    static int obeliskState = 0;
    public GameObject ObeliskModelSolved;
    public GameObject ObeliskModelUnsolved;

    // Start is called before the first frame update
    void Start()
    {
        obeliskState = obeliskState + 1;
        Debug.Log(obeliskState);
    }

    // Update is called once per frame
    void Update()
    {
        if(obeliskState == 2)
        {
            ObeliskModelSolved.SetActive(true);
            ObeliskModelUnsolved.SetActive(false);            
        }
    }
}
