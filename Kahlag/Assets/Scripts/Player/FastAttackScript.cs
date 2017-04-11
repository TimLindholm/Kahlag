using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAttackScript : MonoBehaviour
{

    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;
	
	void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
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
        _actionRef.InAction = true;
        anim.SetBool("FastAttack", true);
        yield return new WaitForSeconds(.3f);
          
        //Execute Action - continue as long as FastAttack = true!
        _actionRef.InAction = false;
        anim.SetBool("FastAttack", false);
    }
}
