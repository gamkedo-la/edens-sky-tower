using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public string[] ThisDialog;
    public DialogManager DM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DM.StartDialog(ThisDialog);
        Destroy(gameObject);
    }
}