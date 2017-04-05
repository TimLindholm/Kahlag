using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitContext
{
	public Enemy Enemy { get; set; }
	//Add more common systems needed by the states here
}

public abstract class StateBehaviour : MonoBehaviour
{
	public string Key;
	public UnitContext Context { get; set; }
	public StateMachine StateMachine { get; set; }

	public abstract void OnEnter();
	public abstract void OnExit();
}
