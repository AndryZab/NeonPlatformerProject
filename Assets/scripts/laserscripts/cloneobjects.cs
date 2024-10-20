using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloneobjects : MonoBehaviour
{
    public GameObject cloneobject; 
    public int count = 2; 
    public float seconds = 0;

    void Start()
    {
        StartCoroutine(objectclone());
    }

    private IEnumerator objectclone()
    {
        int clonesToCreate = count;

        while (clonesToCreate > 0)
        {
            yield return new WaitForSeconds(seconds);
            GameObject newClone = Instantiate(cloneobject, transform.position, Quaternion.identity);
            newClone.transform.SetParent(cloneobject.transform.parent);
            newClone.transform.localScale = cloneobject.transform.localScale;
            clonesToCreate--;

        }
    }

}
