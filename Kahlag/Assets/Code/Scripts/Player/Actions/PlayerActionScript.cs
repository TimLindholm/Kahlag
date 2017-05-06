using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionScript : MonoBehaviour
{

    public bool InAction;

    private FastAttackScript _fastRef;
    private HeavyAttackScript _heavyRef;
	
	void Start ()
    {
        _fastRef = GetComponent<FastAttackScript>();
        _heavyRef = GetComponent<HeavyAttackScript>();
	}
	
	
	void Update ()
    {
        //if(_fastRef.IsFastAttacking == true || _heavyRef.IsHeavyAttacking == true)
        //{
        //    InAction = true;
        //}
        //else
        //{
        //    InAction = false;
        //}

       
        //Debug.Log(InAction);
	}
}
