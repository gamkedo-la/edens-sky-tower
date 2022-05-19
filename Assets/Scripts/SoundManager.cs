using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip storyNarrationOne, storyNarrationTwo, storyNarrationThree, storyNarrationFour, storyNarrationFive, storyNarrationSix, storyNarrationSeven;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        storyNarrationOne = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_One");
        storyNarrationTwo = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_Two");
        storyNarrationThree = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_Three");
        storyNarrationFour = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_Four");
        storyNarrationFive = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_Five");
        storyNarrationSix = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_Six");
        storyNarrationSeven = Resources.Load<AudioClip>("EdensSkyTower_StoryFile_Seven");

        audioSrc = GetComponent<AudioSource>();
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
            case "narrationThree":
                audioSrc.PlayOneShot(storyNarrationThree);
                break;
            case "narrationFour":
                audioSrc.PlayOneShot(storyNarrationFour);
                break;
            case "narrationFive":
                audioSrc.PlayOneShot(storyNarrationFive);
                break;
            case "narrationSix":
                audioSrc.PlayOneShot(storyNarrationSix);
                break;
            case "narrationSeven":
                audioSrc.PlayOneShot(storyNarrationSeven);
                break;
        }
    }
    
}
