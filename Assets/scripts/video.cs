using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerExample : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    void Start()
    {
        
        RenderTexture renderTexture = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 0);

        
        videoPlayer.targetTexture = renderTexture;

        
        rawImage.texture = renderTexture;

        
        Button playButton = GetComponent<Button>();
        playButton.onClick.AddListener(PlayVideo);
    }

    void PlayVideo()
    {
        
        videoPlayer.Play();
    }
}
