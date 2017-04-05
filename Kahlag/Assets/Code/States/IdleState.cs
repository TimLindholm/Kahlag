using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBehaviour
{
	Vector3 m_randomizedPos;

    public override void OnEnter()
	{
		//Debug.Log("Enter Idle State");
		m_randomizedPos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-10f, 25f));
    }

	public override void OnExit()
	{
		//Debug.Log("Exit Idle State");
	}

	void Update ()
	{
		if (Context.Enemy.CanSeePlayer())
		{
			StateMachine.GoToState("CombatState"); 
		}
		else
		{
            
            Context.Enemy.MoveTowardsPosition(m_randomizedPos);
        }
	}
}
