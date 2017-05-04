using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAttackState : StateBehaviour
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


    float horizontalAttackCurve;
    public GameObject damagecoll;


    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        HorizontalAttackCurve();

        m_timer -= Time.deltaTime;
        //TEST
        if (m_timer > 1.55f)
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
        Context.Enemy.anim.SetTrigger("Horizontal");
        StartCoroutine(DamageCollActive());
        Context.Enemy.ActionTimer = cooldown;
    }

    public override void OnExit()
    {
        Context.Enemy.inAttack = false;
    }

    IEnumerator DamageCollActive()
    {
    
        yield return new WaitForSeconds(1.3f);

        
        //_meleeAttackColl = Instantiate(meleeAttackColl, attackPos.transform.position, attackPos.transform.rotation);
      
        yield return new WaitForSeconds(.3f);
  
        Context.Enemy.inAttack = false;
    }

    public void HorizontalAttackCurve()
    {
        horizontalAttackCurve = Context.Enemy.anim.GetFloat("horizontalAttackCurve");
        if (horizontalAttackCurve > 0.5f)
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
