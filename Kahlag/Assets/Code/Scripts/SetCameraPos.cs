using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPos : MonoBehaviour {
    public Transform root;
    public GameMaster gameMasterRef;
    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMasterRef.GameStarted == true)
        {
            transform.position = root.position;
            transform.rotation = root.rotation;
        }

    }
}
