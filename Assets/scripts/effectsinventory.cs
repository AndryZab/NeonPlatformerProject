using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectsinventory : MonoBehaviour
{
    public GameObject[] particleTrail;
    public AudioClip[] audioClips;
    public GameObject canvasinventoryeffect;
    public GameObject canvasinventory;
    [SerializeField] public AudioSource effectsource;

    private GameObject lastActiveTrail; 

    private void Update()
    {
        bool anyActiveTrail = false; 

        for (int i = 0; i < particleTrail.Length; i++)
        {
            if (particleTrail[i].activeSelf && canvasinventory.activeSelf && canvasinventoryeffect.activeSelf)
            {
                anyActiveTrail = true; 

                if (!effectsource.isPlaying)
                {
                    PlaySFX(audioClips[i]); 
                    lastActiveTrail = particleTrail[i];     
                }
                else if (particleTrail[i] != lastActiveTrail)
                {
                    StopSFX(); 
                    PlaySFX(audioClips[i]); 
                    lastActiveTrail = particleTrail[i]; 
                }
            }
        }

        if (!anyActiveTrail && effectsource.isPlaying)
        {
            StopSFX();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        effectsource.clip = clip;
        effectsource.Play();
    }

    public void StopSFX()
    {
        
        if (effectsource.isPlaying)
        {
            
            effectsource.Stop();
        }
    }
}
