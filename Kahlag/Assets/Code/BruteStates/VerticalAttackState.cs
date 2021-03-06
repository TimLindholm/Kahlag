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


    //VFX and Shake
    public GameObject groundSmashParticle;
    private GameObject _spawnedParticle;
    public Transform spawnPoint;

    public bool Cam_Shake = true;
    public float amplitude = 0.1f;
    public float duration = 0.3f;


    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AttackCurve();
        m_timer -= Time.deltaTime;
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


    IEnumerator DamageCollActive()
    {
        yield return new WaitForSeconds(.3f);
        Invoke("Feedback", .8f);
        Context.Enemy.inAttack = false;
    }

    public void AttackCurve()
    {
        attackCurve = Context.Enemy.anim.GetFloat("attackCurve");
        if (attackCurve > 0.5f)
        {
            damagecoll.SetActive(true);         
           
        }
        else
        {
            if (damagecoll.activeInHierarchy)
            {
                damagecoll.SetActive(false);
            }
        }

    }

    public void Feedback()
    {
        _spawnedParticle = Instantiate(groundSmashParticle, spawnPoint.position, spawnPoint.rotation);
        if (Cam_Shake == true)
        {
            CameraShake.Instance.Shake(amplitude, duration);
        }
    }

    public void SetVFXActive()
    {
        AttackVFX.SetActive(true);
    }

    public void DeactiveVFX()
    {
        AttackVFX.SetActive(false);
    }

}


