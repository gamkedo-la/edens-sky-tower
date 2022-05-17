using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public string[] ThisDialog;
    public DialogManager DM;
    public GameObject StoryTabletParticleSystem;
    public string VoiceStoryFile;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("dialog collider collided");
        DM.StartDialog(ThisDialog);
        Destroy(gameObject); // comment out to be able to engage with story tablet once more
        StoryTabletParticleSystem.SetActive(false);

        switch (VoiceStoryFile) //managing which voice files plays
        {
            case "1":
                SoundManager.PlaySound("narrationOne");
                break;
            case "2":
                SoundManager.PlaySound("narrationTwo");
                break;
            case "3":
                SoundManager.PlaySound("narrationThree");
                break;
            case "4":
                SoundManager.PlaySound("narrationFour");
                break;
            case "5":
                SoundManager.PlaySound("narrationFive");
                break;
            case "6":
                SoundManager.PlaySound("narrationSix");
                break;
            case "7":
                SoundManager.PlaySound("narrationSeven");
                break;
        }
    }
}
