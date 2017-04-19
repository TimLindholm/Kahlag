using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAttackScript : MonoBehaviour
{

    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;

    public bool IsSwinging;

    //Shake
    public float amplitude = 0.1f;
    public float duration = 0.5f;

    public bool InCombo;

    

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
        UpdateFastAttack();
    }

    public void UpdateFastAttack()
    {
        if(_actionRef.InAction == false && _ir.FastAttack == true)
        {
            
            StartCoroutine(FastAttack());
        }
    }

    IEnumerator FastAttack()
    {
        CancelInvoke("LeaveActionState");
        IsSwinging = true;
        _actionRef.InAction = true;
        int randomValue = Random.Range(0, 2);

        if (_detectRef.EnemyToTarget != null)
        {
            transform.LookAt(_detectRef.EnemyToTarget);
            anim.SetTrigger("FastAttackTrigger");
            //anim.SetBool("FastAttack", true);
            
            
            anim.SetInteger("AttackType", randomValue);
            
        }
        else
        {
            //anim.SetBool("FastAttack", true);
            anim.SetTrigger("FastAttackTrigger");
            //int randomValue = Random.Range(0, 2);
            anim.SetInteger("AttackType", randomValue);

        }

        //CameraShake.Instance.Shake(amplitude, duration);
       
        yield return new WaitForEndOfFrame();
    
        yield return new WaitForSeconds(.02f);

        if (_ir.FastAttack == true)
        {
            StartCoroutine(FastAttack());
        }
        //yield return new WaitForSeconds(.02f);
        //yield return new WaitForSeconds(.5f);

        IsSwinging = false;
        //_actionRef.InAction = false;
        Invoke("LeaveActionState", .2f);
        anim.SetBool("FastAttack", false);
        yield return new WaitForSeconds(.2f);
    }

    public void LeaveActionState()
    {
        _actionRef.InAction = false;
    }
}
