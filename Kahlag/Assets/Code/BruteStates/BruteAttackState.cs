﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAttackState : StateBehaviour
{
    public float m_huntTime = 5f;

    float m_timer = 0f;
    public bool IsRunningCR;




    public override void OnEnter()
    {
   
        m_timer = m_huntTime;
    }

    public override void OnExit()
    {
    
    }

    private void Update()
    {

        if (IsRunningCR == false)
        {
            StartCoroutine(RandomRot());
        }
    }


    IEnumerator RandomRot()
    {
        IsRunningCR = true;
        yield return new WaitForSeconds(1.5f);
        Context.Enemy.RandomizeRotation();
        IsRunningCR = false;

    }

    void FixedUpdate()
    {
        
        //ChargeTest
        if(Context.Enemy.ChargeCooldown <= 0f)
        {
            StateMachine.GoToState("ChargeState");
        }
        
        if (Context.Enemy.ActionTimer <= 0f)
        {
            //StateMachine.GoToState("Vertical");


            Context.Enemy.RandomizeBruteAttack();
            // Execute attack based on nr.
            if (Context.Enemy.randomAttack == 0)
            {
                //Do attack1
                StateMachine.GoToState("Horizontal");
            }
            else
            {
                //Do attack2
                StateMachine.GoToState("Vertical");
            }
        }

        else if (!Context.Enemy.CanAttackPlayer())
        {
            m_huntTime -= Time.deltaTime;
            if (m_huntTime <= 0f)
            {
                StateMachine.GoToState("CombatState");
                // Charge if possible?
            }
        }

        else
        {
            //Context.Enemy.TakeAim();

            Context.Enemy.RotateAroundTarget(); //<-- Rotates Around Target
        }
    }
}
