using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackScript : MonoBehaviour
{
    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;

    public bool IsSwinging;

    //Shake
    public float amplitude = 0.1f;
    public float duration = 0.5f;

    public bool InCombo;

    public GameObject HeavyAttackColl;

  

    private DetectEnemyScript _detectRef;

    void Start()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
        _detectRef = GetComponent<DetectEnemyScript>();


    }


    void Update()
    {
        UpdateHeavyAttack();
        InAttackCombo();
    }

    public void UpdateHeavyAttack()
    {
        if (_actionRef.InAction == false && _ir.HeavyAttack == true)
        {

            StartCoroutine(HeavyAttack());
        }
    }

    IEnumerator HeavyAttack()
    {
        _actionRef.InAction = true;
        CancelInvoke("LeaveActionState");
        
        //TAKE BACK!
        HeavyAttackColl.SetActive(false);

        IsSwinging = true;
        _actionRef.InAction = true;
      

        if (_detectRef.EnemyToTarget != null)
        {
            transform.LookAt(_detectRef.EnemyToTarget);       
           
        }

        anim.SetTrigger("HeavyAttackTrigger");


        //if (_ir.HeavyAttack == true)
        //{
        //        StartCoroutine(HeavyAttack());
        //}

        //CameraShake.Instance.Shake(amplitude, duration);

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1f);


        //TAKE BACK!
        HeavyAttackColl.SetActive(true);


        //yield return new WaitForSeconds(.02f);

        if (_ir.HeavyAttack == true)
        {
            //StartCoroutine(HeavyAttack());
        }


        //yield return new WaitForSeconds()
        //_actionRef.InAction = false;
        Invoke("LeaveActionState", .4f);
        _actionRef.InAction = true;
        //yield return new WaitForSeconds(.2f);
        //FastAttackColl.SetActive(false);

        yield return new WaitForSeconds(.3f);
        //IsSwinging = false;

        //TAKE BACK!
        HeavyAttackColl.SetActive(false);


        //_actionRef.InAction = false;
        
    }

    public void LeaveActionState()
    {      
        _actionRef.InAction = false;
        IsSwinging = false;
    }

    public void InAttackCombo()
    {
        if (_ir.HeavyAttack == true && IsSwinging == true)
        {
            StartCoroutine(HeavyAttack());
        }
    }


}
