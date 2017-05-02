using System;
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

    UnityEngine.AI.NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        //TEST
        if (m_timer > .9f)
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
    }

    public override void OnExit()
    {
        Context.Enemy.inAttack = false;
    }

    IEnumerator DamageCollActive()
    {
        //yield return new WaitForSeconds(.2f);

        //TEST
      
        yield return new WaitForSeconds(.7f);

        //_rb.AddForce(0, 0, -150f, ForceMode.Impulse);
        _meleeAttackColl = Instantiate(meleeAttackColl, attackPos.transform.position, attackPos.transform.rotation);
        //damageColl.SetActive(true);
        yield return new WaitForSeconds(.3f);
        //damageColl.SetActive(false);
        Context.Enemy.inAttack = false;
    }
}
