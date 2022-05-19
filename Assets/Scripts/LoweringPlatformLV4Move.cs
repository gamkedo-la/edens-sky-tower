using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoweringPlatformLV4Move : MonoBehaviour
{
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string PLATFORM_LOWER = "LV4LoweringPlatform";

    void Awake ()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    public void lowerPlatform()
    {
        animatorComponent.Play(PLATFORM_LOWER);
    }
}
