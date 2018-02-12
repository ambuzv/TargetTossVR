using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallReset : MonoBehaviour {

    public SteamVR_LoadLevel loadlevel;
    public Transform initial;
    private Rigidbody rigid;
    public bool is_Legit;
    public GameObject Stars;
    public List<GameObject> Corner;
    public static int count;
    public List<Material> Mats;
    public HandInteraction handint1;
    public HandInteraction_other handint2;
    public List<AudioClip> Audio;
    // Use this for initialization
    void Start() {
        rigid = gameObject.GetComponent<Rigidbody>();
        count = Stars.transform.childCount;

    }

    public float Sign(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.z - p3.z) - (p2.x - p3.x) * (p1.z - p3.z);
    }

    public bool PointInArea(Vector3 pt, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        bool b1, b2, b3, b4;

        b1 = Sign(pt, v1, v2) < 0.0f;
        b2 = Sign(pt, v2, v3) < 0.0f;
        b3 = Sign(pt, v3, v4) < 0.0f;
        b4 = Sign(pt, v4, v1) < 0.0f;


        return ((b1 == b2) && (b2 == b3) && (b3 == b4));
    }

    // Update is called once per frame
    void Update() {
        if (count == 0)
        {
            Debug.Log("ChangeLevel");
            Invoke("CelebrationSound", 1);
            Invoke("Changelevel", 2);
        }


        //HandInteraction_other handint = obj.GetComponent<HandInteraction_other>();


        is_Legit = PointInArea(gameObject.transform.position, Corner[0].transform.position, Corner[1].transform.position, Corner[2].transform.position, Corner[3].transform.position);


        if (is_Legit == false && (handint1.is_Grabbed == 1 || handint2.is_Grabbed == 1))
        {
            gameObject.GetComponent<Renderer>().material = Mats[1];
            gameObject.layer = 10;
        }

    }

    public void Changelevel()
    {
        loadlevel.Trigger();
    }


    public void CelebrationSound()
    {
        GetComponent<AudioSource>().PlayOneShot(Audio[2], 1);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.tag == "Ground")
        {
            //Debug.Log("YES");
            HitsGround();
        }

       

    }

    public void Failed()
    {
        
        for (int i = 0; i < Stars.transform.childCount; i++)
        {
            Stars.transform.GetChild(i).gameObject.SetActive(true);
        }
        count = Stars.transform.childCount;
    }

    public void HitsStar()
    {
        Backtoposition();
    }

    public void HitsGround()
    {
        Failed();
        Backtoposition();
    }

    public void Backtoposition()
    {
        gameObject.GetComponent<Renderer>().material = Mats[0];
        gameObject.transform.position = initial.position;
        gameObject.layer = 9;
        rigid.velocity = new Vector3(0F, 0F, 0F);
        rigid.isKinematic = true;
    }

    public void AffirmativeSound()
    {
        GetComponent<AudioSource>().PlayOneShot(Audio[0], 1);
    }

    public void NegativeSound()
    {
        GetComponent<AudioSource>().PlayOneShot(Audio[1], 1);
    }

}
