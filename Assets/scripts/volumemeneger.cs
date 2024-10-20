using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumemeneger : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicslider;
    [SerializeField] private Slider musicSFXslider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusisVolume();
            SetSFXVolume();
        }
    }

    public void SetMusisVolume()
    {
        float volume = musicslider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        float volume = musicSFXslider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        myMixer.SetFloat("effect", Mathf.Log10(volume) * 20);

        
        float laserVolume = Mathf.Log10(volume) * 20;  
        laserVolume = Mathf.Clamp(laserVolume, -80, -8); 
        myMixer.SetFloat("laser", laserVolume);

        float laserFollow = Mathf.Log10(volume) * 20;
        laserFollow = Mathf.Clamp(laserFollow, -80, -8);
        myMixer.SetFloat("laserfollow", laserFollow);

        float laserGun = Mathf.Log10(volume) * 20;  
        laserGun = Mathf.Clamp(laserGun, -80, -19); 
        myMixer.SetFloat("lasergun", laserGun);

        float shield = Mathf.Log10(volume) * 20;  
        shield = Mathf.Clamp(shield, -80, -9); 
        myMixer.SetFloat("shield", shield);

        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("lasergunvolume", laserGun);
        PlayerPrefs.SetFloat("effectVolume", volume);
        PlayerPrefs.SetFloat("laserVolume", laserVolume);
        PlayerPrefs.SetFloat("laserFollow", laserFollow);
        PlayerPrefs.SetFloat("shieldVolume", shield);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        musicslider.value = PlayerPrefs.GetFloat("musicVolume");
        musicSFXslider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusisVolume();
        SetSFXVolume();
    }
}
