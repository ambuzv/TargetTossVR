using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {
    public SteamVR_LoadLevel loadlevel;
    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;
    public int currentObject = 0;
	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
        {
            //objectList.Add(child.gameObject);
        }
	}

    public void MenuShow()
    {
        objectList[currentObject].SetActive(true);
    }

    public void MenuDisappear()
    {
        objectList[currentObject].SetActive(false);
    }

	public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
            currentObject = objectList.Count - 1;
        objectList[currentObject].SetActive(true);
    }

    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > objectList.Count - 1)
            currentObject = 0;
        objectList[currentObject].SetActive(true);
    }

    public void SpawnCurrentObject()
    {
        Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectList[currentObject].transform.rotation);
        //Debug.Log(objectList.Count);
        //loadlevel.Trigger();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
