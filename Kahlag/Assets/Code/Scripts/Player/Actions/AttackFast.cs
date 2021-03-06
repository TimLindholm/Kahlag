﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFast : MonoBehaviour
{

    // References
    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;
    private DetectEnemyScript _detectRef;


    public bool InAttack;
    public Transform attackPos;
    public GameObject fastAttackColl;
    public GameObject heavyAttackColl;
    private float fastAttackCurve;
    private float heavyAttackCurve;

    public float FastAttackTimer = 1;
    public float HeavyAttackTimer = 1;
    public float _timer;

    private PlayerStaminaScript _staminaRef;
    public float FastCost = 1;
    public float HeavyCost = 2;


    //Particle
    public GameObject FastParticle1;
    public GameObject FastParticle2;
    public GameObject HeavyParticle1;

    private float fastParticleCurve1;
    private float fastParticleCurve2;
    private float heavyParticleCurve;

    [FMODUnity.EventRef]
    public string VyriaFastEvent;
    FMOD.Studio.EventInstance Vyria_Attack_Fast;

    [FMODUnity.EventRef]
    public string VyriaHeavyEvent;
    FMOD.Studio.EventInstance Vyria_Attack_Heavy;


    public bool AutoLockOn;

    void VyriaFast()
    {
        FMODUnity.RuntimeManager.PlayOneShot(VyriaFastEvent, GetComponent<Transform>().position);
          
    }

    void VyriaHeavy()
    {
        FMODUnity.RuntimeManager.PlayOneShot(VyriaHeavyEvent, GetComponent<Transform>().position);
    }

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _staminaRef = GetComponent<PlayerStaminaScript>();
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
        _detectRef = GetComponent<DetectEnemyScript>();
    }
	
	
	void Update ()
    {
        UpdateFastAttack();
        UpdateHeavyAttack();
        HandleCollider();
        HandleParticle();
        HandleTimer();
        InCombo();
    }

    public void UpdateFastAttack()
    {
        if(_actionRef.InAction == false && _ir.FastAttack == true && _staminaRef.CurrentStamina > FastCost)
        {      
            _timer = FastAttackTimer;
            InAttack = true;
            _actionRef.InAction = true;
            anim.SetTrigger("FastAttackTrigger");
            //FastParticle1.SetActive(true);
            //FastParticle2.SetActive(true);
            
            _staminaRef.CurrentStamina -= FastCost; // <--- Stamina cost

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }
    }

    public void UpdateHeavyAttack()
    {
        if (_actionRef.InAction == false && _ir.HeavyAttack == true && _staminaRef.CurrentStamina > HeavyCost)
        {
            _timer = HeavyAttackTimer;
            InAttack = true;
            _actionRef.InAction = true;
            anim.SetTrigger("HeavyAttackTrigger");
            _staminaRef.CurrentStamina -= HeavyCost; // <--- Stamina cost

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }
        

        //FLOAT
        if (_actionRef.InAction == false && _ir.HeavyAttackFloat < -0.5 && _staminaRef.CurrentStamina > HeavyCost)
        {
            _timer = HeavyAttackTimer;
            InAttack = true;
            _actionRef.InAction = true;
            anim.SetTrigger("HeavyAttackTrigger");
            _staminaRef.CurrentStamina -= HeavyCost; // <--- Stamina cost

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }
    }

    public void InCombo()
    {
        if(_timer < .8f && _ir.FastAttack == true && InAttack == true && _staminaRef.CurrentStamina > FastCost)
        {
            _timer = FastAttackTimer;
            _actionRef.InAction = true;
            anim.SetTrigger("FastAttackTrigger");
            _staminaRef.CurrentStamina -= FastCost; // <--- Stamina cost

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }

        if(_timer < .8f &&  _ir.HeavyAttackFloat < -0.5 && InAttack == true && _staminaRef.CurrentStamina > HeavyCost)
        {
            _timer = HeavyAttackTimer;
            _actionRef.InAction = true; 
            anim.SetTrigger("HeavyAttackTrigger");
            _staminaRef.CurrentStamina -= HeavyCost; // <--- Stamina cost

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }

        if (_timer < .8f && _ir.HeavyAttack == true && InAttack == true && _staminaRef.CurrentStamina > HeavyCost)
        {
            _timer = HeavyAttackTimer;
            _actionRef.InAction = true;
            anim.SetTrigger("HeavyAttackTrigger");
            _staminaRef.CurrentStamina -= HeavyCost; // <--- Stamina cost

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }



    }

    public void HandleCollider()
    {
        
        fastAttackCurve = anim.GetFloat("fastAttackCurve");
        if (fastAttackCurve > 0.5f)
        {
            fastAttackColl.SetActive(true);
            print("whatuuup");
        }
        else
        {
            if (fastAttackColl.activeInHierarchy)
            {
                fastAttackColl.SetActive(false);            
            }
        }

        heavyAttackCurve = anim.GetFloat("heavyAttackCurve");
        if (heavyAttackCurve > 0.5f)
        {
            heavyAttackColl.SetActive(true);
           
        }
        else
        {
            if (heavyAttackColl.activeInHierarchy)
            {
                heavyAttackColl.SetActive(false);              
            }
        }
    }



    public void HandleParticle()
    {

        fastParticleCurve1 = anim.GetFloat("fastParticleCurve1");
        if (fastParticleCurve1 > 0.1f)
        {
            FastParticle1.SetActive(true);
            Invoke("DeactivateVFX", .5f);
        }   

        fastParticleCurve2 = anim.GetFloat("fastParticleCurve2");
        if (fastParticleCurve2 > 0.1f)
        {
            //FastParticle2.SetActive(true);
            Invoke("DeactivateVFX", .5f);
        }


        if (_ir.HeavyAttackFloat < -.5f)
        {
            CancelInvoke("DeactivateVFX");
            Invoke("HeavyVFX", .6f);
            Invoke("DeactivateVFX", 1f);
        }
    }

    
    public void HandleTimer()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
            if(_timer < .3f)
            {
                _actionRef.InAction = false;
            }
        }
        
        else
        {
            _actionRef.InAction = false;
            InAttack = false;
        }
    }

    public void ActivateVFX()
    {
        FastParticle1.SetActive(true);
        //FastParticle2.SetActive(true);
    }

    public void HeavyVFX()
    {
        FastParticle1.SetActive(true);
    }

    public void DeactivateVFX()
    {
        FastParticle1.SetActive(false);
        FastParticle2.SetActive(false);
    }
}
