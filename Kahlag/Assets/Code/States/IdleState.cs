using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBehaviour
{
	Vector3 m_randomizedPos;
    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
	{
		//Debug.Log("Enter Idle State");
		//m_randomizedPos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-10f, 25f));
        agent.Resume();
    }

	public override void OnExit()
	{
        //Debug.Log("Exit Idle State");
        agent.Stop();
    }

    public void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
    }

	void Update ()
	{
		if (Context.Enemy.CanSeePlayer())
		{
            agent.Stop();
            StateMachine.GoToState("CombatState"); 
		}
		else
		{
            
            Context.Enemy.MoveTowardsPosition(m_randomizedPos);
        }
	}
}
