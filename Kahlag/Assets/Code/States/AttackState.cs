using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBehaviour
{
	public float m_huntTime = 5f;

	float m_timer = 0f;

    public override void OnEnter()
	{        
        //Debug.Log("Enter Attack State");
		m_timer = m_huntTime;
    }

	public override void OnExit()
	{
		//Debug.Log("Exit Attack State");
	}

	private void Update()
	{
		//m_timer -= Time.deltaTime;
		//if (m_timer < 0)
		//	StateMachine.GoToState("111234");
	}

	void FixedUpdate ()
	{
        //Try to execute Action, else - MoveTowardsTarget!
        if (Context.Enemy.ActionTimer <= 0f)
        {
           StateMachine.GoToState("MeleeSwing");               
        }
        
        else if (!Context.Enemy.CanAttackPlayer())
        {
            m_huntTime -= Time.deltaTime;
            if(m_huntTime <= 0f)
            {
                StateMachine.GoToState("CombatState");
            }
        }

        else
        {
            Context.Enemy.RotateAroundTarget();
        }


        
        //Context.Enemy      
        ////Navmesh Testing
        //agent.SetDestination(Player.position);
        //Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);
    }
}
