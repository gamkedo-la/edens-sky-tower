using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogColliderForLastObelisk : MonoBehaviour
{
    public string[] ThisDialog;
    public DialogManager DM;
    public InventoryUI sotryTabletUICount;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(sotryTabletUICount.StoryCount <= 6)
            {
                //Debug.Log("dialog collider collided");
                DM.StartDialog(ThisDialog);

            } else
            {
                return;
            }
            
        }       
    }
}
