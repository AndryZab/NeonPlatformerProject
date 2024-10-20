using UnityEngine;

public class ShowEffectsInventory : MonoBehaviour
{
    public GameObject[] objectsToShow;


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
}
