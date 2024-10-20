using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class item : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;
    public GameObject laserdoor;
    public Image checkpanel;
    public RawImage substance;
    public float fadeSpeed = 1f;
    public TextMeshProUGUI checkmatterytext;
    public TextMeshProUGUI  allitemstext;
    private bool isFading = false;
    private int pickedItemsCount = 0;
    Audiomanager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
    }
    private void Start()
    {

        Allitems();

       

    }
    private void invisible()
    {
        float alphaPanel = Mathf.MoveTowards(checkpanel.color.a, 0, fadeSpeed * Time.deltaTime);
        checkpanel.color = new Color(checkpanel.color.r, checkpanel.color.g, checkpanel.color.b, alphaPanel);

        float alphaText = Mathf.MoveTowards(checkmatterytext.color.a, 0, fadeSpeed * Time.deltaTime);
        checkmatterytext.color = new Color(checkmatterytext.color.r, checkmatterytext.color.g, checkmatterytext.color.b, alphaText);

        float alphaText1 = Mathf.MoveTowards(allitemstext.color.a, 0, fadeSpeed * Time.deltaTime);
        allitemstext.color = new Color(allitemstext.color.r, allitemstext.color.g, allitemstext.color.b, alphaText);

        

        float alphaSubstance = Mathf.MoveTowards(substance.color.a, 0, fadeSpeed * Time.deltaTime);
        substance.color = new Color(substance.color.r, substance.color.g, substance.color.b, alphaSubstance);

        if (alphaPanel <= 0f && alphaText <= 0f && alphaSubstance <= 0f && alphaText1 <= 0f)
        {
            isFading = false;
        }
    }

    private void Update()
    {
       
        if (objectToDestroy == null && objectToDestroy1 == null && objectToDestroy2 == null)
        {
            isFading = true;
            DestroyLaserDoor();
        }
        if (isFading)
        {
            invisible();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item") )
        {
            audiomanager.PlaySFX(audiomanager.interactitem);
            pickedItemsCount++;
            UpdateObjectiveText();
            DestroyObject();
        }
        if (collision.gameObject.CompareTag("item1"))
        {
            audiomanager.PlaySFX(audiomanager.interactitem);
            pickedItemsCount++;
            UpdateObjectiveText();
            DestroyObject1();
        }
        if (collision.gameObject.CompareTag("item2"))
        {
            audiomanager.PlaySFX(audiomanager.interactitem);
            pickedItemsCount++;
            UpdateObjectiveText();
            DestroyObject2();
        }
    }
    private void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
    private void DestroyObject1()
    {
        if (objectToDestroy1 != null)
        {
            Destroy(objectToDestroy1);
        }
    }

    private void DestroyObject2()
    {
        if (objectToDestroy2 != null)
        {
            Destroy(objectToDestroy2);
        }
    }
    private void DestroyLaserDoor()
    {
        if (laserdoor != null)
        {
            Destroy(laserdoor);
        }
    }

    private void Allitems()
    {
        int totalItemsWithTagItem = GameObject.FindGameObjectsWithTag("item").Length; 
        int totalItemsWithTagItem1 = GameObject.FindGameObjectsWithTag("item1").Length;
        int totalItemsWithTagItem2 = GameObject.FindGameObjectsWithTag("item2").Length;

        int totalItems = totalItemsWithTagItem + totalItemsWithTagItem1 + totalItemsWithTagItem2;
        allitemstext.text = "/" + totalItems.ToString();
    }
    void UpdateObjectiveText()
    {



        checkmatterytext.text = pickedItemsCount.ToString(); 



    }


}
