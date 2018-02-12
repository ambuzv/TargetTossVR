using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Type : MonoBehaviour {

    public List<Material> mats;

	public void Changecolor()
    {
        gameObject.GetComponent<MeshRenderer>().material = mats[1];
    }


}
