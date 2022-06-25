using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAnimationsScripts : MonoBehaviour
{

    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string ENDSCENE_FADEIN_BACKGROUND = "FadingInEndScene";
    private const string ENDSCENE_FADEIN_TEXT = "FadingIntext";
    private const string STARTSCENE_FADEOUT_BACKGROUND = "FadeOutStartScene";


    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    public void EndSceneFadeInBackg()
    {
        
        animatorComponent.Play(ENDSCENE_FADEIN_BACKGROUND);
    }

    public void EndSceneFadeInText()
    {
        animatorComponent.Play(ENDSCENE_FADEIN_TEXT);
    }

    public void StartSceneFadeOutBackg()
    {
        Debug.Log("FadeIn animation should run");
        animatorComponent.Play(STARTSCENE_FADEOUT_BACKGROUND);
    }


}
