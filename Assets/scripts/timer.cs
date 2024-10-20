using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static levelmenu;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public LayerMask finishLayer;
    public Player playerMovement;
    public GameObject finishcanvas;
    private Rigidbody2D rb;
    public GameObject objectToShow1;
    public GameObject objectToShow2;
    public GameObject objectToShow3;

    public TextMeshProUGUI bestTimeTextforpause;
    public TextMeshProUGUI bestTimeTextforfinish;

    public PlayerDeath buttonpause;
    public ParticleTrail scripteffects;

    [SerializeField] private GameObject hideresult;
    [SerializeField] private int coinsEarneddiamond = 11;
    [SerializeField] private int coinsEarnedgold = 11;
    [SerializeField] private int coinsEarnedsilver = 11;
    [SerializeField] private TextMeshProUGUI yorearned;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerTextInOtherCanvas;
    [SerializeField] private TextMeshProUGUI timerTextFinish;
    public int rewardCoroutineCount1 = 0;
    public int rewardCoroutineCount2 = 0;
    public int rewardCoroutineCount3 = 0;

    [SerializeField] private float maxTimeToShowReward = 0f;
    [SerializeField] private float minTimeToShowReward = 0f;

    private float elapsedTime;
    private Animator anim;
    private bool isTimerRunning = true;
    private bool hasFinished = false;
    Audiomanager audiomanager;
    private Shop Instance;
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
    }
    private void Start()
    {
        rewardCoroutineCount3 = PlayerPrefs.GetInt("RewardCoroutineCount3", 0);
        rewardCoroutineCount2 = PlayerPrefs.GetInt("RewardCoroutineCount2", 0);
        rewardCoroutineCount1 = PlayerPrefs.GetInt("RewardCoroutineCount1", 0);


        Instance = FindObjectOfType<Shop>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        string sceneKey = "BestLevelCompletionTime" + sceneIndex;

        bestTimeTextforpause.text = "Best Time: " + PlayerPrefs.GetFloat(sceneKey).ToString("F2");
        bestTimeTextforfinish.text = "Best Time: " + PlayerPrefs.GetFloat(sceneKey).ToString("F2");


        if (finishcanvas != null)
        {
            finishcanvas.SetActive(false);
        }

        FinishCanvasController.OnFinishCanvasDisplayed += HandleFinishCanvasDisplayed;
    }

    private void OnDestroy()
    {
        FinishCanvasController.OnFinishCanvasDisplayed -= HandleFinishCanvasDisplayed;
    }

    private void HandleFinishCanvasDisplayed()
    {
        StartCoroutine(RewardCoroutine());

    }

    private bool Finish(GameObject obj)
    {
        return obj.CompareTag("finish") || obj.layer == LayerMask.NameToLayer("Finish");
    }

    private void endgame()
    {
        buttonpause.pausebuttonactiveoff.interactable = false;
        foreach (GameObject trail in scripteffects.particleTrail)
        {
            trail.SetActive(false);
        }
        scripteffects.effectsource.Stop();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        audiomanager.musicsource.clip = audiomanager.background;
        audiomanager.musicsource.Stop();
        audiomanager.PlaySFX(audiomanager.finish);
        playerMovement.DisableFlip();
        Invoke(nameof(FinishCanvas), 0f);
        isTimerRunning = false;
        UnlockNewLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("finish"))
        {
            endgame();
            hasFinished = true;
            string sceneKey = "BestLevelCompletionTime" + SceneManager.GetActiveScene().buildIndex;

            float bestTime = PlayerPrefs.GetFloat(sceneKey, Mathf.Infinity);
            if (elapsedTime <= bestTime)
            {
                bestTime = elapsedTime;
                PlayerPrefs.SetFloat(sceneKey, bestTime);
                PlayerPrefs.Save();
                bestTimeTextforpause.text = "Best Time: " + PlayerPrefs.GetFloat(sceneKey).ToString("F2");
                bestTimeTextforfinish.text = "Best Time: " + PlayerPrefs.GetFloat(sceneKey).ToString("F2");

            }



        }
    }

    public void Nextbutton()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndexs"))
        {
            PlayerPrefs.SetInt("ReachedIndexs", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }


    }




    private void FinishCanvas()
    {
        if (finishcanvas != null)
        {
            finishcanvas.SetActive(true);
            FinishCanvasController.NotifyFinishCanvasDisplayed();
        }
    }

    private void timer()
    {
        if (!hasFinished)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);


            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
            timerTextInOtherCanvas.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
            timerTextFinish.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    private void Update()
    {

        if (isTimerRunning && !IsDeathAnimationPlaying())
        {
            timer();
        }
    }

    private IEnumerator RewardCoroutine()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (elapsedTime <= minTimeToShowReward)
        {
            rewardCoroutineCount3++;
            int reachedIndex3 = PlayerPrefs.GetInt("ReachedIndexs3");
            PlayerPrefs.SetInt("RewardCoroutineCount3", rewardCoroutineCount3);
            if (currentIndex >= reachedIndex3)
            {

                PlayerPrefs.SetInt("ReachedIndexs3", currentIndex);
            }
            PlayerPrefs.Save();
            Instance.coinsBalance += coinsEarneddiamond;
            Instance.SaveCoinsBalance();
            Instance.UpdateCoinsUI();
            yield return new WaitForSeconds(0.3f);
            objectToShow1.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            yield return new WaitForSeconds(1f);
            objectToShow2.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            yield return new WaitForSeconds(1f);
            objectToShow3.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            hideresult.SetActive(true);
            yorearned.text = "Matter Earned: " + coinsEarneddiamond;
        }

        else if (elapsedTime > minTimeToShowReward && elapsedTime <= maxTimeToShowReward)
        {
            rewardCoroutineCount2++;
            int reachedIndex2 = PlayerPrefs.GetInt("ReachedIndexs2", 0);
            PlayerPrefs.SetInt("RewardCoroutineCount2", rewardCoroutineCount2);

            if (currentIndex >= reachedIndex2)
            {
                PlayerPrefs.SetInt("ReachedIndexs2", currentIndex);
                PlayerPrefs.Save();
            }

            Instance.coinsBalance += coinsEarnedgold;
            Instance.SaveCoinsBalance();
            Instance.UpdateCoinsUI();
            yield return new WaitForSeconds(0.3f);
            objectToShow1.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            yield return new WaitForSeconds(1f);
            objectToShow2.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            yield return new WaitForSeconds(1f);
            SetObjectColor(objectToShow3, "#242424");
            objectToShow3.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            hideresult.SetActive(true);
            yorearned.text = "Matter Earned: " + coinsEarnedgold;
        }

        else
        {
            rewardCoroutineCount1++;
            int reachedIndex1 = PlayerPrefs.GetInt("ReachedIndexs1", 0);
            PlayerPrefs.SetInt("RewardCoroutineCount1", rewardCoroutineCount1);
            if (currentIndex >= reachedIndex1)
            {
                PlayerPrefs.SetInt("ReachedIndexs1", currentIndex);
                PlayerPrefs.Save();
            }
            Instance.coinsBalance += coinsEarnedsilver;
            Instance.SaveCoinsBalance();
            Instance.UpdateCoinsUI();
            yield return new WaitForSeconds(0.3f);
            objectToShow1.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            yield return new WaitForSeconds(1f);
            SetObjectColor(objectToShow2, "#242424");
            objectToShow2.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            yield return new WaitForSeconds(1f);
            SetObjectColor(objectToShow3, "#242424");
            objectToShow3.SetActive(true);
            audiomanager.PlaySFX(audiomanager.starreward);
            hideresult.SetActive(true);
            yorearned.text = "Matter Earned: " + coinsEarnedsilver;
        }
    }




    private void SetObjectColor(GameObject gameObject, string hexColor)
    {
        if (gameObject != null)
        {
            Graphic graphic = gameObject.GetComponent<Graphic>();
            if (graphic != null)
            {
                Color targetColor;

                if (ColorUtility.TryParseHtmlString(hexColor, out targetColor))
                {
                    graphic.color = targetColor;
                }
            }
        }
    }

    private bool IsDeathAnimationPlaying()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsTag("death");
    }


    public static class FinishCanvasController
    {
        public delegate void FinishCanvasDisplayedDelegate();
        public static event FinishCanvasDisplayedDelegate OnFinishCanvasDisplayed;

        public static void NotifyFinishCanvasDisplayed()
        {
            OnFinishCanvasDisplayed?.Invoke();
        }
    }
}

