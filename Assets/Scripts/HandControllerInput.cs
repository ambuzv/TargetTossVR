using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandControllerInput : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;
    public float yNudgeAmount = 0f; //specific rto teleoprtaimerobject height

    //Teleporter
    private LineRenderer laser;
    private GameObject teleportAimerObject;
    public List<GameObject> teleportAimerlist;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;


    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        laser = GetComponentInChildren<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
       
       
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(true);
                //teleportAimerObject = teleportAimerlist[0];
                //teleportAimerObject.SetActive(true);
                teleportAimerlist[0].SetActive(true);
                laser.SetPosition(0, transform.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask))
                {
                //Debug.Log("In range");
                //Debug.Log(laserMask);
                    teleportLocation = hit.point;
                    laser.SetPosition(1, teleportLocation);
                if (hit.transform.gameObject.layer == 11)
                {
                    Debug.Log("Item");
                    //teleportAimerObject = teleportAimerlist[1];
                    teleportAimerlist[1].SetActive(true);
                    teleportAimerlist[0].SetActive(false);
                    teleportAimerlist[1].transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
                }
                //aimer position
                if (hit.transform.gameObject.layer == 8)
                {
                    Debug.Log("Ground");
                    teleportAimerlist[1].SetActive(false);
                    teleportAimerlist[0].SetActive(true);
                    teleportAimerlist[0].transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
                }
                    //teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);

                }

                else
                {
                    //Debug.Log("Out of range");
                    teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, transform.forward.y * 15 + transform.position.y, transform.forward.z * 15 + transform.position.z);
                    RaycastHit groundRay;
                    if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 200, laserMask))
                    {
                        teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);
                        laser.SetPosition(1, transform.forward * 15 + transform.position);
                        //aimer position
                        teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
                    }


                }
            }
        
       

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            laser.gameObject.SetActive(false);
            teleportAimerlist[0].SetActive(false);
            teleportAimerlist[1].SetActive(false);
            player.transform.position = teleportLocation;
            
        }
        
    }
}
