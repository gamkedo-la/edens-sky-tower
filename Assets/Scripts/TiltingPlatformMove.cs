using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingPlatformMove : MonoBehaviour
{
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string PLATFORM_TILT_FORWARD = "TiltingPlatformForward";
    private const string PLATFORM_TILT_BACKWARD = "TiltingPlatformBackward";


    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    public void tiltPlatformForward ()
    {
        animatorComponent.Play(PLATFORM_TILT_FORWARD);
    }

    public void tiltPlatformBackwards()
    {
        animatorComponent.Play(PLATFORM_TILT_BACKWARD);
    }
}
