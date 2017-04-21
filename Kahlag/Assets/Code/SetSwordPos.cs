using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSwordPos : MonoBehaviour {

    public Transform root;
	// Use this for initialization
	void Start () {
        transform.position = root.position;
        transform.rotation = root.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = root.position;
        //transform.rotation = root.rotation;
    }
}
