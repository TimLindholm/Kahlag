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


    private Rigidbody _rb;

    //Attack
    public Transform attackPos;
    public Rigidbody meleeAttackColl;
    private Rigidbody _meleeAttackColl;


    UnityEngine.AI.NavMeshAgent agent;

    float attackCurve;
    public GameObject damagecoll;

    public override void OnEnter()
    {
        agent.Stop();
        m_timer = m_actionTime; //Use Animation Length?
        Context.Enemy.RotateAroundTarget();
        Context.Enemy.inAttack = true;
        //Random Action Cooldown
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);
        //Debug.Log("MeleeSwing");
        Context.Enemy.anim.SetBool("MeleeSwing", true);
        Context.Enemy.anim.SetTrigger("MeleeAttack");
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
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AttackCurve();

        m_timer -= Time.deltaTime;
        //TEST
        if(m_timer > 0.55f)
        {
            Context.Enemy.TakeAim();
        }
      
        if (m_timer < 0)
            
            StateMachine.GoToState("AttackState");
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
            print("Active");
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
