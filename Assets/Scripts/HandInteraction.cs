using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public float ThrowForce = 1.5f;
    public GameObject text;
    public BallReset ballreset;

    //Swipe
    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedleft;
    public bool hasSwipedright;
    public ObjectMenuManager objectMenuManager;
    public int is_Grabbed = 0;
    public bool is_Thrown;
    public AudioClip scroll;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            FirstTouch();
            touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            text.GetComponent<TextMesh>().text = "";
            touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            distance = touchCurrent - touchLast;
            touchLast = touchCurrent;
            swipeSum += distance;
           // if (!hasSwipedright)
            {
                if (swipeSum > 0.2)
                {
                    swipeSum = 0;
                    SwipeRight();
                    hasSwipedright = true;
                    hasSwipedleft = false;
                }
            }
            //if (!hasSwipedleft)
            {
                if (swipeSum < -0.2)
                {
                    swipeSum = 0;
                    SwipeLeft();
                    hasSwipedleft = true;
                    hasSwipedright = false;
                }
            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                SpawnObject();
            }

        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            text.GetComponent<TextMesh>().text = "Items";
            swipeSum = 0;
            touchCurrent = 0;
            touchLast = 0;
            hasSwipedleft = false;
            hasSwipedright = false;
            LastTouch();
        }

             
    }

    void SpawnObject()
    {
        //Debug.Log(objectMenuManager.currentObject);
        objectMenuManager.SpawnCurrentObject();
    }

    void FirstTouch()
    {
        objectMenuManager.MenuShow();
    }

    void LastTouch()
    {
        objectMenuManager.MenuDisappear();
    }

    void SwipeLeft()
    {
        objectMenuManager.MenuLeft();
        //Debug.Log("Swiped right");
        GetComponent<AudioSource>().PlayOneShot(scroll, 1);
    }
    void SwipeRight()
    {
        objectMenuManager.MenuRight();
        //Debug.Log("Swiped left");
        GetComponent<AudioSource>().PlayOneShot(scroll, 1);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(col);
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(col);
            }
            if(device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                is_Grabbed = 1;
                //Debug.Log("Pressed");
                //GameObject obj = GameObject.Find("Ball");
                //BallReset ballreset = obj.GetComponent<BallReset>();
                //if (ballreset.is_Legit == false)
                //{
                //    ballreset.gameObject.GetComponent<Renderer>().material = ballreset.Mats[1];
                //    ballreset.gameObject.layer = 10;
                //}
            }
        }
    }

    void GrabObject(Collider coli)
    {
        coli.transform.SetParent(gameObject.transform);
        coli.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(2000);
        gameObject.GetComponent<AudioSource>().Play();
        Debug.Log("touching down the trigger");
    }

    void ThrowObject(Collider coli)
    {
        coli.transform.SetParent(null);
        Rigidbody rigidbody = coli.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = device.velocity * ThrowForce;
        rigidbody.angularVelocity = device.angularVelocity;
        is_Thrown = true;
        is_Grabbed = 0;
        Debug.Log("released the trigger");
    }

}
