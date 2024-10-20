using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas canvas;
    public Button rewardbutton;
    public Button watchvideobutton;
    public GameObject panel;
    public bool allowrotate = false;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

        rewardbutton.gameObject.SetActive(false);
        panel.SetActive(false);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        rewardbutton.gameObject.SetActive(true);
        panel.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        videoPlayer.Play();
        rewardbutton.gameObject.SetActive(false);
        panel.SetActive(false);
    }

    public void videobutton()
    {
       watchvideobutton.interactable = false;
       allowrotate = true;
    }
    
}
