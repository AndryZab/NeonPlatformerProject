using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectsToManage; 
    public GameObject[] objectsToShow;
    public GameObject ButtonEquip;
    public GameObject ButtonUnequip;

    private void Start()
    {
        
        LoadObjectStates(); 
    }

    public void LoadObjectStates()
    {
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            string key = "Object" + i + "_ActiveState";

            
            if (PlayerPrefs.HasKey(key))
            {
                
                int objectActiveState = PlayerPrefs.GetInt(key);

                
                objectsToManage[i].SetActive(objectActiveState == 1);
            }
        }

        if (ButtonEquip != null && ButtonEquip.activeSelf &&
            ButtonUnequip != null && ButtonUnequip.activeSelf &&
            objectsToShow.Length >= 2 &&
            objectsToShow[0] != null && objectsToShow[0].activeSelf &&
            objectsToShow[1] != null && objectsToShow[1].activeSelf)
        {
            if (PlayerPrefs.HasKey("idleshowforbuy"))
            {
                int objectToShow1 = PlayerPrefs.GetInt("idleshowforbuy", 0);
                objectsToShow[1].SetActive(objectToShow1 == 1);
            }

            if (PlayerPrefs.HasKey("ShowButtonforbuy"))
            {
                bool ShowButtonforbu = PlayerPrefs.GetInt("ShowButtonforbuy", 0) == 1;
                ButtonEquip.SetActive(ShowButtonforbu);
            }
        }
        
        if (PlayerPrefs.HasKey("EquipButtonState") && ButtonEquip != null)
        {
            bool equipButtonState = PlayerPrefs.GetInt("EquipButtonState", 0) == 1;
            ButtonEquip.SetActive(equipButtonState);
        }

        if (PlayerPrefs.HasKey("UnequipButtonState") && ButtonUnequip != null)
        {
            bool unequipButtonState = PlayerPrefs.GetInt("UnequipButtonState", 0) == 1;
            ButtonUnequip.SetActive(unequipButtonState);
        }

        if (PlayerPrefs.HasKey("ObjectToShow0_ActiveState") && objectsToShow != null && objectsToShow.Length > 0)
        {
            int objectToShow0State = PlayerPrefs.GetInt("ObjectToShow0_ActiveState", 0);
            objectsToShow[0].SetActive(objectToShow0State == 1);
        }

        if (PlayerPrefs.HasKey("ObjectToShow1_ActiveState") && objectsToShow != null && objectsToShow.Length > 0)
        {
            int objectToShow1State = PlayerPrefs.GetInt("ObjectToShow1_ActiveState", 0);
            objectsToShow[1].SetActive(objectToShow1State == 1);
        }
    }


    private void SaveObjectStates()
    {
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            PlayerPrefs.SetInt("Object" + i + "_ActiveState", objectsToManage[i].activeSelf ? 1 : 0); // Збереження станів активності об'єктів
        }
        PlayerPrefs.Save();
    }

    public void ToggleObjectActive(int objectIndex)
    {
        if (objectIndex >= 0 && objectIndex < objectsToManage.Length)
        {
            DeactivateAllObjects(); 
            objectsToManage[objectIndex].SetActive(true); 
        }
        
    }

    public void EquipObjectToPrefab()
    {
        if (ButtonEquip.activeSelf) 
        {
            GameObject activeObject = null;
            
            for (int i = 0; i < objectsToManage.Length; i++)
            {
                if (objectsToManage[i].activeSelf)
                {
                    activeObject = objectsToManage[i];
                    break;
                }
            }

            if (activeObject != null)
            {
                
                SaveObjectStates(); 

                ToggleObjects();

                PlayerPrefs.SetInt("ObjectToShow0_ActiveState", objectsToShow[0].activeSelf ? 1 : 0);
                PlayerPrefs.SetInt("ObjectToShow1_ActiveState", objectsToShow[1].activeSelf ? 1 : 0);

                ButtonEquip.SetActive(false);
                ButtonUnequip.SetActive(true);
                PlayerPrefs.SetInt("EquipButtonState", 0); 
                PlayerPrefs.SetInt("UnequipButtonState", 1); 
                PlayerPrefs.Save();
                LoadObjectStates();
            }
           
        }
    }

    public void UnequipObjectFromPrefab()
    {
                DeactivateAllObjects();    

                
                for (int i = 0; i < objectsToManage.Length; i++)
                {
                    PlayerPrefs.DeleteKey("Object" + i + "_ActiveState");
                }
                PlayerPrefs.DeleteKey("EquipButtonState");
                PlayerPrefs.DeleteKey("UnequipButtonState");
                PlayerPrefs.DeleteKey("ObjectToShow0_ActiveState");
                PlayerPrefs.DeleteKey("ObjectToShow1_ActiveState");
                PlayerPrefs.Save();

                ToggleObjectsIdle();

                PlayerPrefs.SetInt("ObjectToShow0_ActiveState", objectsToShow[0].activeSelf ? 1 : 0);
                PlayerPrefs.SetInt("ObjectToShow1_ActiveState", objectsToShow[1].activeSelf ? 1 : 0);

                
                ButtonEquip.SetActive(true);
                ButtonUnequip.SetActive(false);

                PlayerPrefs.SetInt("EquipButtonState", 1); 
                PlayerPrefs.SetInt("UnequipButtonState", 0); 
                PlayerPrefs.Save();
                LoadObjectStates();
            
            
        
    }



    public void ToggleObjects()
    {
        objectsToShow[0].SetActive(true);
        objectsToShow[1].SetActive(false);
    }

    public void ToggleObjectsIdle()
    {
        objectsToShow[0].SetActive(false);
        objectsToShow[1].SetActive(true);
    }

    public void DeactivateAllObjects()
    {
        for (int i = 0; i < objectsToManage.Length; i++)
        {
            objectsToManage[i].SetActive(false);
        }
    }

    public void buyitemshowbuttonequip()
    {
        if (ButtonEquip.activeSelf || ButtonUnequip.activeSelf || objectsToShow[0].activeSelf || objectsToShow[1].activeSelf)
        {
            return;
        }

        ButtonEquip.SetActive(true);
        objectsToShow[1].SetActive(true);
        PlayerPrefs.SetInt("ShowButtonforbuy", 1); 
        PlayerPrefs.SetInt("idleshowforbuy", objectsToShow[1].activeSelf ? 1 : 0);     
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        SaveObjectStates(); 
    }
}
