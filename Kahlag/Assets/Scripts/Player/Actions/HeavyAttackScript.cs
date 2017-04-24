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

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
        _detectRef = GetComponent<DetectEnemyScript>();

    }




    void Update ()
    {
        UpdateHeavyAttack();
        CheckIfSwinging();
        HandleTimer();
        InAttackCombo();
        HandleCollider();

    }

    public void UpdateHeavyAttack()
    {
        if (_actionRef.InAction == false && IsHeavyAttacking == false && _ir.HeavyAttack == true)
        {

            StartCoroutine(HeavyAttack());
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
        IsSwinging = true;
        anim.SetTrigger("HeavyAttackTrigger");
        yield return new WaitForSeconds(1f);
        _attackColl = Instantiate(hAttackColl, attackPos.transform.position, attackPos.transform.rotation);
    }

    public void InAttackCombo()
    {
        if (_ir.HeavyAttack == true && IsSwinging == true)
        {
            //StopCoroutine(FastAttack());
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
            //anim.applyRootMotion = true;
            //FastAttackColl.SetActive(true);
            IsHeavyAttacking = true;
            //_actionRef.InAction = true;
        }
        else
        {
            //anim.applyRootMotion = false;
            //_actionRef.InAction = false;
            IsHeavyAttacking = false;
            //FastAttackColl.SetActive(false);
            IsSwinging = false;
        }
    }

    public void CheckIfSwinging()
    {
        //if(IsHeavyAttacking == true)
        //{
        //    _actionRef.InAction = true;
        //}
        //else if(IsHeavyAttacking == false)
        //{
        //    _actionRef.InAction = false;
        //}
    }
}
