using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboState : StateBehaviour
{
    public float m_actionTime;
    float m_timer = 0f;

    public float MinCD = 2f;
    public float MaxCD = 4f;

    public float ComboCD = 10f;

    private Rigidbody _rb;

   


    UnityEngine.AI.NavMeshAgent agent;

    float comboAttackCurve;
    public GameObject damagecoll;



    // MOVE TEST
    public Transform comboTarget;
    public Transform startPos;

    public float speed = 1.0F;



    void Update()
    {
        AttackCurve();
        AdjustSpeed();

        m_timer -= Time.deltaTime;
        
        if (m_timer > 2f)
        {
            Context.Enemy.TakeAim();
        }

        if (m_timer < 0)

            StateMachine.GoToState("AttackState");

    }



    public override void OnEnter()
    {
        Context.Enemy.inAttack = true;
        comboTarget.position = Context.Enemy.m_target.position;
        startPos.position = transform.position;
        agent.Stop();
        m_timer = m_actionTime;
        
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);

        Context.Enemy.anim.SetTrigger("Combo");
        StartCoroutine(DamageCollActive());
        Context.Enemy.ActionTimer = cooldown;
        Context.Enemy.ComboCooldown = ComboCD;


    }

    public override void OnExit()
    {
        Context.Enemy.inAttack = false;
        damagecoll.SetActive(false);
    }

    IEnumerator DamageCollActive()
    {
        //yield return new WaitForSeconds(.2f);

        //TEST

        //yield return new WaitForSeconds(.7f);


        //_meleeAttackColl = Instantiate(meleeAttackColl, attackPos.transform.position, attackPos.transform.rotation);
        yield return new WaitForSeconds(1f);
        comboTarget.position = Context.Enemy.m_target.position;


        yield return new WaitForSeconds(2f);

        Context.Enemy.inAttack = false;
    }

    public void AttackCurve()
    {
        comboAttackCurve = Context.Enemy.anim.GetFloat("comboAttackCurve");
        if (comboAttackCurve > 0.5f)
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

    public void AdjustSpeed()
    {
  

        if(m_timer <= 1.8f)
        {
            
            transform.position = Vector3.Lerp(startPos.position, comboTarget.position, speed * Time.deltaTime);
            print("lerp");
        }    
    }



    void Awake ()
    {


        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }
	

}
