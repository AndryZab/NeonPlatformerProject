using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{

    [SerializeField] public AudioSource musicsource;
    [SerializeField] public AudioSource SFXsource;
    public AudioClip background;
    public AudioClip backgroundcavern;
    public AudioClip menubuttons;
    public AudioClip walltouch;
    public AudioClip jump;
    public AudioClip buyitems;
    public AudioClip equipitems;
    public AudioClip interactitem;
    public AudioClip exitboard;
    public AudioClip finish;
    public AudioClip death;
    public AudioClip starreward;
    public AudioClip buttonlaser;
    public AudioClip trampoline;

    public void Start()
    {
            musicsource.clip = background;
            musicsource.Play();
        

    }
    public void Stopmusic()
    {
        musicsource.clip = background;
        musicsource.Stop();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }

}
