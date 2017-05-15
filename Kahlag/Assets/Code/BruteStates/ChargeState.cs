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

    [FMODUnity.EventRef]
    public string BossGrowlEvent;
    FMOD.Studio.EventInstance Growl;

    [FMODUnity.EventRef]
    public string BossGroundImpactEvent;
    FMOD.Studio.EventInstance Impact_Ground;

    [FMODUnity.EventRef]
    public string BossVerticalSwooshEvent;
    FMOD.Studio.EventInstance Attack_Vertical;

    [FMODUnity.EventRef]
    public string BossHorizontalSwooshEvent;
    FMOD.Studio.EventInstance Attack_Horizontal;

    [FMODUnity.EventRef]
    public string BossFootEvent;
    FMOD.Studio.EventInstance Boss_Footsteps;

    float comboAttackCurve;
    public GameObject ChargeCollider;
    public GameObject SwingCollider;

    public Transform chargeTarget;
    public Transform startPos;

    
    public float ChargeSpeed = 1;

    float chargeAimCurve;
    float chargeAttackCurve;
    public bool IsCharging;

    public bool TurnInCharge;

    UnityEngine.AI.NavMeshAgent agent;


    //VFX and shake
    public bool Cam_Shake = true;
    public float amplitude = 0.1f;
    public float duration = 0.3f;

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
        Invoke("Feedback", .4f);
        //Context.Enemy.ChargeCooldown = ChargeCD; ---- ADD ChargeCooldown to Enemy Script

    }
    public override void OnExit()

    {
        chargeTarget.position = transform.position;
        Context.Enemy.inAttack = false;
        ChargeCollider.SetActive(false);
    }

    void Boss_Growl()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossGrowlEvent,GetComponent<Transform>().position);
    }

    void Boss_Ground_Impact()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossGroundImpactEvent, GetComponent<Transform>().position);
    }

    void Boss_Vertical_Swoosh()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossVerticalSwooshEvent, GetComponent<Transform>().position);
    }

    void Boss_Horizontal_Swoosh()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossHorizontalSwooshEvent, GetComponent<Transform>().position);
    }

    void Boss_Foot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossFootEvent, GetComponent<Transform>().position);
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

        if (m_timer > .5f)
        {
            Context.Enemy.TakeAim();
        }

        if (m_timer < 0)

            StateMachine.GoToState("AttackState");

    }

    IEnumerator ChargeCollActive()
    {
        yield return new WaitForSeconds(1f);
        startPos.position = transform.position;
        chargeTarget.Translate(0, 0, ChargeDistance);
        yield return new WaitForSeconds(1f);
    }


    public void AimCurve()
    {
        chargeAimCurve = Context.Enemy.anim.GetFloat("chargeAimCurve");
        //Context.Enemy.TakeAim();
        if (chargeAimCurve > 0.7f)
        {
            //if (TurnInCharge)
            //Context.Enemy.TakeAim();
        }
        chargeAttackCurve = Context.Enemy.anim.GetFloat("chargeAttackCurve");
        if (chargeAttackCurve > 0.9f)
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

    public void Feedback()
    {     
        if (Cam_Shake == true)
        {
            CameraShake.Instance.Shake(amplitude, duration);
        }
    }
}
