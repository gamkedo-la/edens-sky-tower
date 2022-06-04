using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAnimationsScripts : MonoBehaviour
{

    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string ENDSCENE_FADEIN_BACKGROUND = "FadingInEndScene";
    private const string ENDSCENE_FADEIN_TEXT = "FadingIntext";


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


}
