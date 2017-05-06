using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSwing : StateBehaviour
{
    public float m_actionTime;
    float m_timer = 0f;

    public float MinCD = 2f;
    public float MaxCD = 4f;


    private Rigidbody _rb;
  


    UnityEngine.AI.NavMeshAgent agent;

    float downswingAttackCurve;
    public GameObject damagecoll;


    public Transform comboTarget;
    public Transform startPos;

    public float speed = .5f;

    public override void OnEnter()
    {
        Context.Enemy.inAttack = true;
        comboTarget.position = Context.Enemy.m_target.position;
        startPos.position = transform.position;
        agent.Stop();

        m_timer = m_actionTime;
        Context.Enemy.RotateAroundTarget();

        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);

        Context.Enemy.anim.SetTrigger("DownSwing");
        StartCoroutine(DamageCollActive());
        Context.Enemy.ActionTimer = cooldown;

        //Perform Action

    }

    public override void OnExit()
    {
        //Context.Enemy.anim.SetBool("MeleeSwing", false);
        Context.Enemy.inAttack = false;
    }

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DownSwingCurve();
        AdjustSpeed();

        m_timer -= Time.deltaTime;
        //TEST
        if (m_timer > 1.1f)
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
        comboTarget.position = Context.Enemy.m_target.position;
        yield return new WaitForSeconds(.3f);

        Context.Enemy.inAttack = false;
    }

    public void DownSwingCurve()
    {
        downswingAttackCurve = Context.Enemy.anim.GetFloat("downswingAttackCurve");
        if (downswingAttackCurve > 0.5f)
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

    public void AdjustSpeed()
    {


        if (m_timer <= 1.2f)
        {
            //float distCovered = (Time.time - startTime) * speed;
            //float fracJourney = distCovered / journeyLength;
            //comboTarget.position = Context.Enemy.m_target.position;
            transform.position = Vector3.Lerp(startPos.position, comboTarget.position, speed * Time.deltaTime);
            print("lerp");
        }
    }
}
