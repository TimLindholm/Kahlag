using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAttackState : StateBehaviour
{
    //CD

    public float MinCD = 2f;
    public float MaxCD = 4f; 

    private Rigidbody _rb;


    //Attack
    public Transform attackPos;
    public Rigidbody meleeAttackColl;
    private Rigidbody _meleeAttackColl;

    public override void OnEnter()
    {
        Context.Enemy.inAttack = true;     
        float cooldown = UnityEngine.Random.Range(MinCD, MaxCD);
        Context.Enemy.anim.SetTrigger("Vertical");
        StartCoroutine(DamageCollActive());
        Context.Enemy.ActionTimer = cooldown;
    }

    public override void OnExit()
    {
        Context.Enemy.inAttack = false;
    }

    IEnumerator DamageCollActive()
    {
        //yield return new WaitForSeconds(.2f);

        //TEST
      
        yield return new WaitForSeconds(.7f);

        //_rb.AddForce(0, 0, -150f, ForceMode.Impulse);
        _meleeAttackColl = Instantiate(meleeAttackColl, attackPos.transform.position, attackPos.transform.rotation);
        //damageColl.SetActive(true);
        yield return new WaitForSeconds(.3f);
        //damageColl.SetActive(false);
        Context.Enemy.inAttack = false;
    }
}
