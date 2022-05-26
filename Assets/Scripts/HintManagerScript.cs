using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManagerScript : MonoBehaviour
{
    [SerializeField] GameObject dialogWindow;
    private DialogManager dialogManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogManagerScript = dialogWindow.GetComponent<DialogManager>();
        //Time.timeScale = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogManagerScript.DialogActive)
            {
                Time.timeScale = 1;
            }
            
            gameObject.SetActive(false);
        }
    }
    */
}
