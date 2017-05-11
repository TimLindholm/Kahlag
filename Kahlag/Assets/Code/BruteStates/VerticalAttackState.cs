﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAttackState : StateBehaviour
{

    public float m_actionTime;
    float m_timer = 0f;


    //CD
    public float MinCD = 2f;
    public float MaxCD = 4f; 

    private Rigidbody _rb;


    //Attack
    public Transform attackPos;
    public Rigidbody meleeAttackColl;
    private Rigidbody _meleeAttackColl;

    public GameObject AttackVFX;

    UnityEngine.AI.NavMeshAgent agent;

    float attackCurve;
    public GameObject damagecoll;


    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AttackCurve();


        m_timer -= Time.deltaTime;
        //TEST
        if (m_timer > 1.15f)
        {      
                Context.Enemy.TakeAim();
        }

        if (m_timer < 0)

            StateMachine.GoToState("AttackState");
    }

    public override void OnEnter()
    {
        Context.Enemy.inAttack = true;
        m_timer = m_actionTime;
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);
        Context.Enemy.anim.SetTrigger("Vertical");
        StartCoroutine(DamageCollActive());
        Context.Enemy.ActionTimer = cooldown;
        Invoke("SetVFXActive", .6f);
        Invoke("DeactiveVFX", 1.3f);
    }

    public override void OnExit()
    {
        Context.Enemy.inAttack = false;
        AttackVFX.SetActive(false);
    }

    public void SetVFXActive()
    {
        AttackVFX.SetActive(true);
    }

    public void DeactiveVFX()
    {
        AttackVFX.SetActive(false);
    }

    IEnumerator DamageCollActive()
    {
        //yield return new WaitForSeconds(.2f);

        //TEST
      
        //yield return new WaitForSeconds(.7f);
        //_meleeAttackColl = Instantiate(meleeAttackColl, attackPos.transform.position, attackPos.transform.rotation);




        yield return new WaitForSeconds(.3f);
       
        Context.Enemy.inAttack = false;
    }

    public void AttackCurve()
    {
        attackCurve = Context.Enemy.anim.GetFloat("attackCurve");
        if (attackCurve > 0.5f)
        {
            damagecoll.SetActive(true);
            
            //print("Active");
        }
        else
        {
            if (damagecoll.activeInHierarchy)
            {
                damagecoll.SetActive(false);
            }
        }

    }

}


