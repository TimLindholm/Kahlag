using System.Collections;
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
    public GameObject SwingParticle1;

    private float fastParticleCurve;
    private float heavyParticleCurve;

  
  

    public bool AutoLockOn;

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
        fastParticleCurve = anim.GetFloat("fastParticleCurve");
        if (fastParticleCurve > 0.1f)
        {
  
            SwingParticle1.SetActive(true);
        }
        else
        {
            if (SwingParticle1.activeInHierarchy)
            {
            
                SwingParticle1.SetActive(false);
            }
        }

        heavyParticleCurve = anim.GetFloat("heavyParticleCurve");
        if (heavyParticleCurve > 0.1f)
        {
          
            SwingParticle1.SetActive(true);
        }
        else
        {
            if (SwingParticle1.activeInHierarchy)
            {
               
                SwingParticle1.SetActive(false);
            }
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
}
