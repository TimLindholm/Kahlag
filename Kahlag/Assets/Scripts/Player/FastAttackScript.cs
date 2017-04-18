using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAttackScript : MonoBehaviour
{

    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;

    public Transform root;

    public bool IsSwinging;


    private float _combotimer;


    //Shake
    public float amplitude = 0.1f;
    public float duration = 0.5f;

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
            IsSwinging = true;
            StartCoroutine(FastAttack());
        }
    }

    IEnumerator FastAttack()
    {
        
        _actionRef.InAction = true;

        if(_detectRef.EnemyToTarget != null)
        {
            transform.LookAt(_detectRef.EnemyToTarget);
            anim.SetBool("FastAttack", true);
            anim.SetTrigger("FastAttack1");
        }
        else
        {
            anim.SetBool("FastAttack", true);
            anim.SetTrigger("FastAttack1");
        }
    
        //CameraShake.Instance.Shake(amplitude, duration);
      
        
        
            

            if(_ir.FastAttack == true)
            {
                //StartCoroutine(FastAttack2());
            }
        
       
        yield return new WaitForSeconds(.5f);
        IsSwinging = false;
        _actionRef.InAction = false;
        anim.SetBool("FastAttack", false);
    }

    IEnumerator FastAttack2()
    {
        
        StopCoroutine(FastAttack());
        anim.SetTrigger("FastAttack2");
        //CameraShake.Instance.Shake(amplitude, duration);
       
        
        
            
            if (_ir.FastAttack == true)
            {
                StartCoroutine(FastAttack3());
            }
        

        yield return new WaitForSeconds(.2f);
        IsSwinging = false;
        _actionRef.InAction = false;
    }

    IEnumerator FastAttack3()
    {
        StopCoroutine(FastAttack2());
        anim.SetTrigger("FastAttack3");
        //yield return new WaitForSeconds(combo2);
        //while (_combo1timer <= Combo1.clip.length)
        //{
        //    if (_ir.FastAttack == true)
        //    {

        //    }
        //}

        yield return new WaitForSeconds(.2f);
        IsSwinging = false;
        _actionRef.InAction = false;
    }
}
