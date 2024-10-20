using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class levelmenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public GameObject[] levelObjects;
    public GameObject[] enabledPanel;
    public GameObject[] stars1;
    public GameObject[] stars2;
    public GameObject[] stars3;
    public GameObject canvasanimforlevle;
    int index1;
    int index2;
    int index3;


    public static levelmenu instance;
    private bool hasMenu = false;

    private void Awake()
    {

        int showCompleted = PlayerPrefs.GetInt("ReachedIndexs");
        int star1 = PlayerPrefs.GetInt("ReachedIndexs1");
        int star2 = PlayerPrefs.GetInt("ReachedIndexs2");
        int star3 = PlayerPrefs.GetInt("ReachedIndexs3");

        int count3star = PlayerPrefs.GetInt("RewardCoroutineCount3");
        int count2star = PlayerPrefs.GetInt("RewardCoroutineCount2");
        int count1star = PlayerPrefs.GetInt("RewardCoroutineCount1");



        index3 = star3 - 1;
        index2 = star2 - 1;
        index1 = star1 - 1;


        for (int i = 0; i < showCompleted; i++)
        {
            levelObjects[i].SetActive(true);
        }
        for (int i = 0; i < star1; i++)
        {
            if (star2 == 0 && star3 == 0 || count1star >= stars3.Length)
            {
                stars1[i].SetActive(true);

            }
            else if (index1 < stars1.Length && count1star <= stars1.Length)
            {
                stars1[index1].SetActive(true);

            }

        }
        for (int i = 0; i < star2; i++)
        {
            if (star1 == 0 && star3 == 0 || count2star >= stars3.Length)
            {
                stars2[i].SetActive(true);
                if (index1 != 0 && stars1.Length > index1 && index1 >= 0)
                {
                    stars1[i].SetActive(false);

                }
            }
            else if (index2 < stars2.Length && count2star != stars2.Length)
            {
                stars2[index2].SetActive(true);
                stars1[index2].SetActive(false);

            }

        }




        for (int i = 0; i < star3; i++)
        {
            if (star1 == 0 && star2 == 0 || count3star >= stars3.Length)
            {


                stars3[i].SetActive(true);
                if (index2 != 0 && stars2.Length > index2 && index2 >= 0)
                {
                    stars2[i].SetActive(false);
                }
                if (index1 != 0 && stars1.Length > index1 && index1 >= 0)
                {
                    stars1[i].SetActive(false);

                }



            }
            else if (index3 < stars3.Length && count3star != stars3.Length)
            {
                stars3[index3].SetActive(true);
                stars1[index3].SetActive(false);
                stars2[index3].SetActive(false);

            }

        }









        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int ib = 0; ib < buttons.Length; ib++)
        {
            buttons[ib].interactable = false;
        }
        for (int ib = 0; ib < unlockedLevel; ib++)
        {
            buttons[ib].interactable = true;
        }


    }




    public void OpenLevel(int levelId)
    {
        if (!hasMenu && levelId > 0)
        {
            StartCoroutine(LevelsAnimAndLoad(levelId));
            hasMenu = true;
        }
    }

    private IEnumerator LevelsAnimAndLoad(int levelId)
    {
        canvasanimforlevle.SetActive(true);
        yield return new WaitForSeconds(1f);

        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
        Debug.Log("Success");
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }
    }

}
