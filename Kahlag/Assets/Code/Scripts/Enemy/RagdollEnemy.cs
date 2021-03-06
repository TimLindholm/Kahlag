﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnemy : MonoBehaviour
{


    public Collider[] cols;
    public Rigidbody[] rigids;
    SkinnedMeshRenderer[] smr;
    Animator anim;
    bool goRagdoll;

    //PlayerControl platformove;

    private Enemy enemy;
    private StateMachine _sm;

    public Transform Body;

    //Cultist
    private CombatState _combat;
    private AttackState _attack;
    private MeleeSwingState _meleeSwing;
    private ComboState _combo;
    private DownSwing _downSwing;
    UnityEngine.AI.NavMeshAgent agent;

    //Brute
    private BruteAttackState _bruteAtt;
    private HorizontalAttackState _hor;
    private VerticalAttackState _vert;
    private ChargeState _charge;

    public bool IsCultist;
    public bool IsBrute;

    public GameObject Weapon;

    // Use this for initialization
    void Start()
    {
        
        anim = GetComponent<Animator>();
        rigids = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();
        enemy = GetComponent<Enemy>();
        _sm = GetComponent<StateMachine>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing 
        _combat = GetComponent<CombatState>();
        smr = GetComponentsInChildren<SkinnedMeshRenderer>();

        if (IsCultist == true)
        {           
            _attack = GetComponent<AttackState>();
            _meleeSwing = GetComponent<MeleeSwingState>();
            _combo = GetComponent<ComboState>();
            _downSwing = GetComponent<DownSwing>();
        }

        if(IsBrute == true)
        {
            _bruteAtt = GetComponent<BruteAttackState>();
            _hor = GetComponent<HorizontalAttackState>();
            _vert = GetComponent<VerticalAttackState>();
            _charge = GetComponent<ChargeState>();

        }

        
        
         
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

    public void RagdollCharacter(Vector3 impulse)
    {
        if (!goRagdoll)
        {
            anim.enabled = false;
            foreach (Rigidbody rig in rigids)
            {
                if (rig.gameObject.layer == 9)
                {
                    rig.isKinematic = false;
                    rig.AddForce(impulse * Random.Range(0.7f, 1f), ForceMode.Impulse);
                    
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

            foreach (var mesh in smr)
            {
                var bounds = mesh.localBounds;
                bounds.extents = Vector3.one * 20;
                mesh.localBounds = bounds;
            }
        }

    }


    public void CloseAllComponents()
    {
        agent.enabled = false;

        if(IsCultist == true)
        {
            Destroy(gameObject);
            if (GetComponent<MeleeSwingState>())
            {
                MeleeSwingState _meleeSwing = GetComponent<MeleeSwingState>();
                _meleeSwing.enabled = false;
            }

            if (GetComponent<DownSwing>())
            {
                DownSwing _downSwing = GetComponent<DownSwing>();
                _downSwing.enabled = false;
            }

            if (GetComponent<ComboState>())
            {
                ComboState _combo = GetComponent<ComboState>();
                _combo.enabled = false;
            }

            if (GetComponent<AttackState>())
            {
                AttackState _attack = GetComponent<AttackState>();
                _attack.enabled = false;
            }


        }

        if(IsBrute == true)
        {

            //Weapon.transform.parent = null;
            Destroy(Weapon);
            Destroy(gameObject);

            if (GetComponent<HorizontalAttackState>())
            {
                HorizontalAttackState _hor = GetComponent<HorizontalAttackState>();
                _hor.enabled = false;
            }

            if (GetComponent<VerticalAttackState>())
            {
                VerticalAttackState _vert = GetComponent<VerticalAttackState>();
                _vert.enabled = false;
            }

            if (GetComponent<ChargeState>())
            {
                ChargeState _charge = GetComponent<ChargeState>();
                _charge.enabled = false;
            }

            if (GetComponent<BruteAttackState>())
            {
                BruteAttackState _bruteAtt = GetComponent<BruteAttackState>();
                _bruteAtt.enabled = false;
            }
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


        Body.transform.parent = null;
        




        //_move.enabled = false;

    }
}
