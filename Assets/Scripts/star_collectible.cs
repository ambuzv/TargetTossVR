using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class star_collectible : MonoBehaviour {
   
    public BallReset ballreset;
    

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

 

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.layer == 9)
        {
            ballreset.Backtoposition();
            gameObject.SetActive(false);
            BallReset.count--;
            ballreset.AffirmativeSound();
        }

        if (other.collider.gameObject.layer == 10)
        {
            ballreset.NegativeSound();
        }
    }

    
}
