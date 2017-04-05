using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : StateBehaviour
{


    public override void OnEnter()
    {
        //Debug.Log("Enter Combat State");
     
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Combat State");
    }

    void Update()
    {
        Context.Enemy.MoveTowardsTarget();
        Context.Enemy.RotateAroundTarget();

        if (Context.Enemy.CanAttackPlayer())
        {
            StateMachine.GoToState("AttackState"); //If within range, try to attack!
        }

        else if(!Context.Enemy.CanSeePlayer())
        {
            StateMachine.GoToState("IdleState"); //If not in vision, return to idle (dont use this if enemy has vision with limited angle)!!
        }
    }
}
