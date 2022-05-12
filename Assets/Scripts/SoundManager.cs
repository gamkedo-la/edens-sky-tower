using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip storyNarrationOne, storyNarrationTwo;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //storyNarrationOne = SFX.Load<AudioClip>("SFX_Ambience_Space_Crickets");
       // storyNarrationTwo = SFX.Load<AudioClip>("SFX_Ambience_Space_Wind");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "narrationOne":
                audioSrc.PlayOneShot(storyNarrationOne);
                    break;
            case "narrationTwo":
                audioSrc.PlayOneShot(storyNarrationTwo);
                    break;
        }
    }
}
