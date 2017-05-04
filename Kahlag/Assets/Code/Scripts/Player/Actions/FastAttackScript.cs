using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAttackScript : MonoBehaviour
{

    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;

    public bool IsSwinging;
    public bool IsFastAttacking;


    public Rigidbody fAttackColl;
    private Rigidbody _attackColl;



    public GameObject attackColl;
    private float fastAttackCurve;


    public float AttackTimer = 1;
    public float _timer;

    public Transform attackPos;
    

    private DetectEnemyScript _detectRef;


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
        AttackCurve();
        HandleTimer();
        InAttackCombo();
        HandleCollider();
    }

    public void AttackCurve()
    {
        fastAttackCurve = anim.GetFloat("fastAttackCurve");
        if (fastAttackCurve > 0.5f)
        {
            attackColl.SetActive(true);
            _actionRef.InAction = true;
            //print("Active");
        }
        else
        {
            if (attackColl.activeInHierarchy)
            {
                attackColl.SetActive(false);
            }
        }

    }

    public void UpdateFastAttack()
    {
        if (_actionRef.InAction == false && IsFastAttacking == false && _ir.FastAttack == true)
        {

            StartCoroutine(FastAttack());
        }
    }

    IEnumerator FastAttack()
    {
        _timer = AttackTimer;
        //_actionRef.InAction = true;
        IsFastAttacking = true;
        //IsSwinging = true;

        

        yield return new WaitForEndOfFrame();

        if(_detectRef.EnemyToTarget != null)
        {
            if(AutoLockOn)
            transform.LookAt(_detectRef.EnemyToTarget);
        }
        IsSwinging = true;
        anim.SetTrigger("FastAttackTrigger");
        yield return new WaitForSeconds(.2f);
        //_attackColl = Instantiate(fAttackColl, attackPos.transform.position, attackPos.transform.rotation);
    }


    public void InAttackCombo()
    {
        if (_ir.FastAttack == true && IsSwinging == true)
        {
            //StopCoroutine(FastAttack());
            StartCoroutine(FastAttack());
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
        if(_timer > 0)
        {
            //anim.applyRootMotion = true;
            //FastAttackColl.SetActive(true);
            //_actionRef.InAction = true;
            IsFastAttacking = true;
        }
        else
        {
            //anim.applyRootMotion = false;
            // _actionRef.InAction = false; 
            IsFastAttacking = false;
            //FastAttackColl.SetActive(false);
            IsSwinging = false;
        }
    }


    //public void HandleTimer()
    //{
    //    if(IsSwinging == true)
    //    {
    //        anim.applyRootMotion = true;
    //        _timer += Time.deltaTime;
    //        if(_timer > AttackTimer)
    //        {
    //            FastAttackColl.SetActive(true);
    //            _timer = 0f;
    //        }
    //        else
    //        {
    //            anim.applyRootMotion = false;
    //            FastAttackColl.SetActive(false);
    //            IsSwinging = false;
    //            _actionRef.InAction = false;
    //        }
    //    }
    //}
    //IEnumerator FastAttack()
    //{
    //    //TAKE BACK
    //    //CancelInvoke("LeaveActionState");

    //    //TAKE BACK!
    //    //FastAttackColl.SetActive(false);

    //    _actionRef.InAction = true;
    //    //int randomValue = Random.Range(0, 2);

    //    if (_detectRef.EnemyToTarget != null)
    //    {
    //        transform.LookAt(_detectRef.EnemyToTarget);
    //        //anim.SetTrigger("FastAttackTrigger");
    //        //anim.SetBool("FastAttack", true);


    //        //anim.SetInteger("AttackType", randomValue);

    //    }
    //    //else
    //    //{
    //    //    //anim.SetBool("FastAttack", true);
    //    //    anim.SetTrigger("FastAttackTrigger");
    //    //    //int randomValue = Random.Range(0, 2);
    //    //    //anim.SetInteger("AttackType", randomValue);

    //    //}
    //    anim.SetTrigger("FastAttackTrigger");
    //    //CameraShake.Instance.Shake(amplitude, duration);
    //    FastAttackColl.SetActive(true);
    //    yield return new WaitForEndOfFrame();
    //    IsSwinging = true;

    //    //TAKE BACK!
    //    FastAttackColl.SetActive(true);
    //    yield return new WaitForSeconds(.02f);
    //    IsSwinging = true;
    //    if (_ir.FastAttack == true)
    //    {
    //        //StartCoroutine(FastAttack());
    //    }
    //    //yield return new WaitForSeconds(.02f);
    //    //yield return new WaitForSeconds(.5f);


    //    //_actionRef.InAction = false;

    //    //TAKE BACK
    //    //Invoke("LeaveActionState", .5f);

    //    //anim.SetBool("FastAttack", false);
    //    yield return new WaitForSeconds(.2f);
    //    _actionRef.InAction = false;
    //    IsSwinging = false;

    //    //TAKE BACK!
    //    FastAttackColl.SetActive(false);
    //}




    //public void HandleCollider()
    //{
    //    if(_ir.FastAttack == true && IsSwinging == true)
    //    {
    //        //anim.applyRootMotion = true;
    //        FastAttackColl.SetActive(true);
    //    }
    //    else
    //    {
    //        //anim.applyRootMotion = false;
    //        FastAttackColl.SetActive(false);
    //    }
    //}

    //public void LeaveActionState()
    //{
    //    _actionRef.InAction = false;
    //    IsSwinging = false;
    //}
}
