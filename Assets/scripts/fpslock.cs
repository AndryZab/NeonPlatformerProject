using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class fpslock: MonoBehaviour
{
    public int frameRate;
    public TextMeshProUGUI fpsShow;

    void Update()
    {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        Application.targetFrameRate = frameRate;
        if (fpsShow != null)
        {
            fpsShow.text = "FPS: " + fps.ToString();

            DebugManager.instance.enableRuntimeUI = false;

        }
    
    }
   




}
