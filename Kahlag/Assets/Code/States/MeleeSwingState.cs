using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwingState : StateBehaviour
{
    public float m_actionTime;
    float m_timer = 0f;

    public float MinCD = 2f;
    public float MaxCD = 4f;
    

    public override void OnEnter()
    {
        m_timer = m_actionTime; //Use Animation Length?


        //Random Action Cooldown
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);
        Debug.Log("MeleeSwing");
        Context.Enemy.anim.SetBool("MeleeSwing", true);
        Context.Enemy.ActionTimer = cooldown;
       
        //Perform Action

    }

    public override void OnExit()
    {
        Context.Enemy.anim.SetBool("MeleeSwing", false);
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
            
            StateMachine.GoToState("AttackState");
    }
}
