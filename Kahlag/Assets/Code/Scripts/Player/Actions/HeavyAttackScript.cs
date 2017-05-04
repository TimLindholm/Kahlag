using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackScript : MonoBehaviour
{

    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;

    public bool IsSwinging;
    public bool IsHeavyAttacking;

    public Rigidbody hAttackColl;
    private Rigidbody _attackColl;


    public float AttackTimer = 1;
    public float _timer;

    public Transform attackPos;


    private DetectEnemyScript _detectRef;
    private PlayerHealthScript _healthRef;


    public GameObject attackColl;
    private float heavyAttackCurve;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
        _detectRef = GetComponent<DetectEnemyScript>();
        _healthRef = GetComponent<PlayerHealthScript>();

    }




    void Update ()
    {
        UpdateHeavyAttack();
        AttackCurve();
        HandleTimer();
        InAttackCombo();
        HandleCollider();
        CheckInteruption();

    }

    public void AttackCurve()
    {
        heavyAttackCurve = anim.GetFloat("heavyAttackCurve");
        if (heavyAttackCurve > 0.5f)
        {
            attackColl.SetActive(true);
            print("Active");
        }
        else
        {
            if (attackColl.activeInHierarchy)
            {
                attackColl.SetActive(false);
            }
        }

    }

    public void UpdateHeavyAttack()
    {
        if (_actionRef.InAction == false && IsHeavyAttacking == false && _ir.HeavyAttack == true)
        {

            StartCoroutine(HeavyAttack());
        }
    }

    public void CheckInteruption()
    {
        if(_timer > 1f && _healthRef.Damaged == true)
        {
            print("Attack Cancelled");
            StopCoroutine(HeavyAttack());
            //_timer = 0f;
        }
    }

    IEnumerator HeavyAttack()
    {
        _timer = AttackTimer;
        //_actionRef.InAction = true;
        IsHeavyAttacking = true;
        //IsSwinging = true;



        yield return new WaitForEndOfFrame();

        if (_detectRef.EnemyToTarget != null)
        {
            transform.LookAt(_detectRef.EnemyToTarget);
        }
        anim.SetTrigger("HeavyAttackTrigger");
        yield return new WaitForSeconds(.6f);
        IsSwinging = true;
        
        if (_detectRef.EnemyToTarget != null)
        {
            transform.LookAt(_detectRef.EnemyToTarget);
        }
        yield return new WaitForSeconds(2f);
        //if (_healthRef.Damaged == false)
            //_attackColl = Instantiate(hAttackColl, attackPos.transform.position, attackPos.transform.rotation);
    }

    public void InAttackCombo()
    {
        if (_ir.HeavyAttack == true && IsSwinging == true)
        {     
            StartCoroutine(HeavyAttack());
        }
    }

    public void HandleTimer()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    public void HandleCollider()
    {
        if (_timer > 0)
        {
           
            IsHeavyAttacking = true;
           
        }
        else
        {
         
            IsHeavyAttacking = false;
            IsSwinging = false;
        }
    }
}
