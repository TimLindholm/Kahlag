﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBehaviour
{
	Vector3 m_randomizedPos;
    UnityEngine.AI.NavMeshAgent agent;


    public bool IsPatrolling;

    public override void OnEnter()
	{
		//Debug.Log("Enter Idle State");
		//m_randomizedPos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-10f, 25f));
        //agent.Resume();
    }

	public override void OnExit()
	{
        //Debug.Log("Exit Idle State");
   
        agent.isStopped = true;
    }

    public void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
    }

	void Update ()
	{
		if (Context.Enemy.CanSeePlayer())
		{
            
            agent.isStopped = true;
            StateMachine.GoToState("CombatState"); 
		}
		else
		{
            if(IsPatrolling)
            Context.Enemy.MoveTowardsPosition(m_randomizedPos);
        }

        if(Context.Enemy.CurrentHealth < Context.Enemy.MaxHealth)
        {
            
            agent.isStopped = true;
            StateMachine.GoToState("CombatState");
        }

        if(Context.Enemy.IsAlerted == true)
        {
            agent.isStopped = true;
            StateMachine.GoToState("CombatState");
        }
	}
}
