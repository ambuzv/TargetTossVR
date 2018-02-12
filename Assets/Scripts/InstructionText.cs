using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionText : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer + Time.deltaTime;
        if (timer < 10.0f)
        {
            gameObject.GetComponent<Text>().text = "Press down trigger button to grab any object and release to throw object \n                         Collect all stars to go to the next level...";
        }
        if (timer > 10.0f)
            gameObject.GetComponent<Text>().text = "";
    }
}
