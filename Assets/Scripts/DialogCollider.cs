using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public string[] ThisDialog;
    public DialogManager DM;
    public GameObject StoryTabletParticleSystem;
    public string VoiceStoryFile;
    public InventoryUI sotryTabletUICount;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("dialog collider collided");
        DM.StartDialog(ThisDialog);
        Destroy(gameObject); // comment out to be able to engage with story tablet once more
        if (StoryTabletParticleSystem != null)
        {
            StoryTabletParticleSystem.SetActive(false);
        }
        

        if(VoiceStoryFile.Length > 0)
        {
            PlayerPrefs.SetInt("seenTablet" + VoiceStoryFile, 1);
        }

        switch (VoiceStoryFile) //managing which voice files plays
        {
            case "1":
                SoundManager.PlaySound("narrationOne");
                sotryTabletUICount.UpdateUIStoryCount();// calls out function that ticks up counter in UI
                break;
            case "2":
                SoundManager.PlaySound("narrationTwo");
                sotryTabletUICount.UpdateUIStoryCount();
                break;
            case "3":
                SoundManager.PlaySound("narrationThree");
                sotryTabletUICount.UpdateUIStoryCount();
                break;
            case "4":
                SoundManager.PlaySound("narrationFour");
                sotryTabletUICount.UpdateUIStoryCount();
                break;
            case "5":
                SoundManager.PlaySound("narrationFive");
                sotryTabletUICount.UpdateUIStoryCount();
                break;
            case "6":
                SoundManager.PlaySound("narrationSix");
                sotryTabletUICount.UpdateUIStoryCount();
                break;
            case "7":
                SoundManager.PlaySound("narrationSeven");
                sotryTabletUICount.UpdateUIStoryCount();
                break;
        }
    }
}
