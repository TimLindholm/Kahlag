using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnemy : MonoBehaviour
{


    public Collider[] cols;
    public Rigidbody[] rigids;
    Animator anim;
    bool goRagdoll;

    //PlayerControl platformove;

    private Enemy enemy;
    private StateMachine _sm;
    private CombatState _combat;
    private AttackState _attack;
    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigids = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();
        enemy = GetComponent<Enemy>();
        _sm = GetComponent<StateMachine>();
        _combat = GetComponent<CombatState>();
        _attack = GetComponent<AttackState>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing  
        //_move = GetComponent<PlayerMovementScript>();


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
        agent.enabled = false;

        if (GetComponent<AttackState>())
        {
            AttackState _attack = GetComponent<AttackState>();
            _attack.enabled = false;
        }


        if (GetComponent<CombatState>())
        {
            CombatState _combat = GetComponent<CombatState>();
            _combat.enabled = false;
        }

        if (GetComponent<Enemy>())
        {
            Enemy enemy = GetComponent<Enemy>();
            enemy.enabled = false;
        }

        if(GetComponent<StateMachine>())
        {
            StateMachine _sm = GetComponent<StateMachine>();
            _sm.enabled = false;
        }


       

        //_move.enabled = false;

    }
}
