using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource[] sources;
    void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    void StepSound()
    {
        sources[0].Play(0);
    }

    void JumpSound()
    {
        sources[1].Play(0);
    }
}
