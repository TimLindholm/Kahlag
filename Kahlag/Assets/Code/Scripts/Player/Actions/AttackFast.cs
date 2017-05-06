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

  
  

    public bool AutoLockOn;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
        _detectRef = GetComponent<DetectEnemyScript>();
    }
	
	
	void Update ()
    {
        UpdateFastAttack();
        UpdateHeavyAttack();
        HandleCollider();
        HandleTimer();
        InCombo();
    }

    public void UpdateFastAttack()
    {
        if(_actionRef.InAction == false && _ir.FastAttack == true)
        {      
            _timer = FastAttackTimer;
            InAttack = true;
            _actionRef.InAction = true;
            anim.SetTrigger("FastAttackTrigger");

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }
    }

    public void UpdateHeavyAttack()
    {
        if (_actionRef.InAction == false && _ir.HeavyAttack == true)
        {
            _timer = HeavyAttackTimer;
            InAttack = true;
            _actionRef.InAction = true;
            anim.SetTrigger("HeavyAttackTrigger");

            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }
    }

    public void InCombo()
    {
        if(_timer < .8f && _ir.FastAttack == true && InAttack == true)
        {
            _timer = FastAttackTimer;
            _actionRef.InAction = true;
            anim.SetTrigger("FastAttackTrigger");


            if (_detectRef.EnemyToTarget != null)
            {
                if (AutoLockOn)
                    transform.LookAt(_detectRef.EnemyToTarget);
            }
        }

        if(_timer < .8f && _ir.HeavyAttack == true && InAttack == true)
        {
            _timer = HeavyAttackTimer;
            _actionRef.InAction = true; 
            anim.SetTrigger("HeavyAttackTrigger");

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
    
    public void HandleTimer()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
            if(_timer < .4f)
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
