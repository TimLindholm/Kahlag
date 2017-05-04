using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : StateBehaviour
{


    public float m_actionTime;
    float m_timer = 0f;

    public float MinCD = 2f;
    public float MaxCD = 4f;

    public float ChargeCD = 25f;

    private Rigidbody _rb;

    public float ChargeDistance = 6;


    

    float comboAttackCurve;
    public GameObject ChargeCollider;
    public GameObject SwingCollider;

    public Transform chargeTarget;
    public Transform startPos;

    
    public float ChargeSpeed = 1;

    float chargeAimCurve;
    float chargeAttackCurve;
    public bool IsCharging;


    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        Context.Enemy.inAttack = true;
        agent.Stop();
        m_timer = m_actionTime;
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);
        Context.Enemy.anim.SetTrigger("ChargeAttack");
        StartCoroutine(ChargeCollActive());
        Context.Enemy.ActionTimer = cooldown;
        Context.Enemy.ChargeCooldown = ChargeCD;
        //Context.Enemy.ChargeCooldown = ChargeCD; ---- ADD ChargeCooldown to Enemy Script

    }
    public override void OnExit()

    {
        chargeTarget.position = transform.position;
        Context.Enemy.inAttack = false;
        ChargeCollider.SetActive(false);
    }
  

    void Awake ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }
	
	
	void Update ()
    {
        AimCurve();
        ChargingForward();

        m_timer -= Time.deltaTime;

        if (m_timer > 2f)
        {
            //Context.Enemy.TakeAim();
        }

        if (m_timer < 0)

            StateMachine.GoToState("AttackState");

    }

    IEnumerator ChargeCollActive()
    {
        //yield return new WaitForSeconds(.2f);

        //TEST

        //yield return new WaitForSeconds(.7f);


        //_meleeAttackColl = Instantiate(meleeAttackColl, attackPos.transform.position, attackPos.transform.rotation);
        //yield return new WaitForSeconds(.5f);
        startPos.position = transform.position;
        //chargeTarget.position = Context.Enemy.m_target.position;

        chargeTarget.Translate(0, 0, ChargeDistance);

        
        //chargeTarget.position = new Vector3(0, 0, 5f);


        yield return new WaitForSeconds(1f);

        //Context.Enemy.inAttack = false;
    }


    public void AimCurve()
    {
        chargeAimCurve = Context.Enemy.anim.GetFloat("chargeAimCurve");
        //Context.Enemy.TakeAim();
        if (chargeAimCurve > 0.7f)
        {
           
            //print("Aiming");
            Context.Enemy.TakeAim();
        }
        chargeAttackCurve = Context.Enemy.anim.GetFloat("chargeAttackCurve");
        if (chargeAttackCurve > 0.7f)
        {
            ChargeCollider.SetActive(true);
            IsCharging = true;

            //print("Active");
            transform.position = Vector3.Lerp(startPos.position, chargeTarget.position, ChargeSpeed * Time.deltaTime);
            //Move here?         
        }
        else
        {
            if (ChargeCollider.activeInHierarchy)
            {
                ChargeCollider.SetActive(false);
                IsCharging = true;
            }
        }

    }

    public void ChargingForward()
    {
        if(m_timer <= .8f)
        {
            transform.position = Vector3.Lerp(startPos.position, chargeTarget.position, ChargeSpeed * Time.deltaTime);
            //agent.Resume();
        }
        else
        {
            //agent.Stop();
        }

        //agent.SetDestination(chargeTarget.position);
        //Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);
    }
}
