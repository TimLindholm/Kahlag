using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageState : StateBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void OnEnter()
    {
        Debug.Log("TakeDamage");
        

    }

    public override void OnExit()
    {
        //Debug.Log("Exit Combat State");
        



    }
}
