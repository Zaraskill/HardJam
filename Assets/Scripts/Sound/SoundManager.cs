using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayNo()
    {
        AkSoundEngine.PostEvent("Play_AllNo", gameObject);
    }

    public void PlayYes()
    {
        AkSoundEngine.PostEvent("Play_AllYes", gameObject);
    }

    public void PlayNext()
    {
        AkSoundEngine.PostEvent("Play_AllNext", gameObject);
    }

    public void Play30sec()
    {
        AkSoundEngine.PostEvent("Play_All30sec", gameObject);
    }

    public void PlayMusique()
    {
        AkSoundEngine.PostEvent("Play_Musique", gameObject);
    }

    public void PlayPause()
    {
        AkSoundEngine.PostEvent("Pause_Musique", gameObject);
    }
}
