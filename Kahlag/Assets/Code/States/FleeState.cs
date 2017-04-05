using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : StateBehaviour
{
	public float m_fleeTime = 5f;

	float m_timer = 0f;

	public override void OnEnter()
	{
		Debug.Log("Enter Flee State");
		m_timer = m_fleeTime;
	}

	public override void OnExit()
	{
		Debug.Log("Exit Flee State");
	}

	private void Update()
	{
		m_timer -= Time.deltaTime;
		if (m_timer < 0)
			StateMachine.GoToState("111234");
	}

	void FixedUpdate ()
	{
		Context.Enemy.MoveAwayFromTarget();
	}
}
