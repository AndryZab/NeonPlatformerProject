using UnityEngine;
using UnityEngine.UIElements;

public class ParticleTrail : MonoBehaviour
{
    public GameObject[] particleTrail;
    public AudioClip[] audioClips;
    [SerializeField] public AudioSource effectsource;
    public Player player;


    private void Update()
    {
        for (int i = 0; i < particleTrail.Length; i++)
        {
            if (audioClips != null && particleTrail[i].activeSelf && !effectsource.isPlaying && Time.timeScale == 1 && player.rb.bodyType != RigidbodyType2D.Static)
            {
                PlaySFX(audioClips[i]); 
                break; 
            }
            

            if (Time.timeScale == 0 || player.rb.bodyType == RigidbodyType2D.Static)
            {
                PauseEffects();
            }


        }
    }

    public void PlaySFX(AudioClip clip)
    {
        effectsource.clip = clip;
        effectsource.Play();
    }

    public void PauseEffects()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (audioClips[i] != null) 
            {
                effectsource.clip = audioClips[i];
                effectsource.Pause();
            }
        }
    }


    public void UnPauseEffects(AudioClip[] clips)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            effectsource.clip = clips[i];
            effectsource.UnPause();
        }
    }






}
