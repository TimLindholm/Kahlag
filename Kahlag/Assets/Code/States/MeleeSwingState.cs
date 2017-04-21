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

    public GameObject damageColl;

    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        agent.Stop();
        m_timer = m_actionTime; //Use Animation Length?
        Context.Enemy.RotateAroundTarget();
        Context.Enemy.inAttack = true;
        //Random Action Cooldown
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);
        Debug.Log("MeleeSwing");
        Context.Enemy.anim.SetBool("MeleeSwing", true);
        StartCoroutine(DamageCollActive());
        Context.Enemy.ActionTimer = cooldown;
       
        //Perform Action

    }

    public override void OnExit()
    {
        Context.Enemy.anim.SetBool("MeleeSwing", false);
        Context.Enemy.inAttack = false;
    }

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
            
            StateMachine.GoToState("AttackState");
    }

    IEnumerator DamageCollActive()
    {
        //yield return new WaitForSeconds(.2f);

        //TEST
        //Context.Enemy.RotateAroundTarget();
        //Context.Enemy.inAttack = true;
        yield return new WaitForSeconds(.8f);      
        damageColl.SetActive(true);
        yield return new WaitForSeconds(.4f);
        damageColl.SetActive(false);
        Context.Enemy.inAttack = false;
    }
}
