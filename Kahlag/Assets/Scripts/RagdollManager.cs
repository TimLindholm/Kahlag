using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{


    public Collider[] cols;
    public Rigidbody[] rigids;
    Animator anim;
    bool goRagdoll;

    //PlayerControl platformove;

    private PlayerMovementScript _move;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigids = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();
        _move = GetComponent<PlayerMovementScript>();
      

        foreach (Rigidbody rig in rigids)
        {
            if (rig.gameObject.layer == 9)
            {
                rig.isKinematic = true;
            }
        }
        foreach (Collider col in cols)
        {
            if (col.gameObject.layer == 9)
            {
                col.isTrigger = true;
            }
        }
    }

    public void RagdollCharacter()
    {
        if (!goRagdoll)
        {
            anim.enabled = false;
            foreach (Rigidbody rig in rigids)
            {
                if (rig.gameObject.layer == 9)
                {
                    rig.isKinematic = false;
                }
            }

            foreach (Collider col in cols)
            {
                if (col.gameObject.layer == 9)
                {
                    col.isTrigger = false;
                }
            }
            goRagdoll = true;
        }

    }


    public void CloseAllComponents()
    {
        if (GetComponent<PlayerMovementScript>())
        {
            PlayerMovementScript platformove = GetComponent<PlayerMovementScript>();
            platformove.enabled = false;
        }

        //_move.enabled = false;


    }
}

