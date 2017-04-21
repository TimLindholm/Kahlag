using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	public UnitContext m_context;
	public string m_startState;

    public float ActionTimer;

	List<StateBehaviour> m_states;

	void Start ()
	{
		//Get all available states on this game object
		m_states = new List<StateBehaviour>(GetComponentsInChildren<StateBehaviour>());

		//Setup context
		m_context.Enemy = GetComponent<Enemy>();

		//Initialize all states and disable them
		foreach (var state in m_states)
		{
			state.StateMachine = this;
			state.Context = m_context;
			state.enabled = false;
		}

		//Go to start state
		GoToState(m_startState);
	}
	
	public void GoToState(string activeState)
	{
        //First exit the previous state
      
        
            foreach (var state in m_states)
            {
                if (state.Key != activeState && state.enabled)
                {
                    state.OnExit();
                    state.enabled = false;
                    break;
                }
            }
        



		//Then enter the new state
		foreach (var state in m_states)
		{
			if (state.Key == activeState && !state.enabled)
			{
				state.OnEnter();
				state.enabled = true;
				break;
			}
		}
	}
}
