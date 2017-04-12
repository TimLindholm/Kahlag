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
            IsSwinging = true;
            StartCoroutine(FastAttack());
        }
    }

    IEnumerator FastAttack()
    {
        _actionRef.InAction = true;
        anim.SetBool("FastAttack", true);
        CameraShake.Instance.Shake(amplitude, duration);
        yield return new WaitForSeconds(.3f);
        IsSwinging = false;
        //Execute Action - continue as long as FastAttack = true!
        _actionRef.InAction = false;
        anim.SetBool("FastAttack", false);
    }
}
