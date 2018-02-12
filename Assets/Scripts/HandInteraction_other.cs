using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction_other : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public float ThrowForce = 1.5f;
    public int is_Grabbed = 0;
    public bool is_Thrown;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
       
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
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
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
        Debug.Log("touching down the trigger");
        gameObject.GetComponent<AudioSource>().Play();
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
