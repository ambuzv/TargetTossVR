using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public SteamVR_LoadLevel loadlevel;
    public static int count;
	// Use this for initialization
	void Start () {
        count = SceneManager.sceneCountInBuildSettings;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextRound(int current)
    {
        
        loadlevel.Trigger();
        
    }

}
