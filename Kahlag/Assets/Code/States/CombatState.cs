using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : StateBehaviour
{

    //Navmesh Testing
    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        Debug.Log("Enter Combat State");
        agent.Resume();

    }

    public override void OnExit()
    {
        Debug.Log("Exit Combat State");
        agent.Stop();



    }

    public void Start()
    {
        
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
    }

    void Update()
    {
        Context.Enemy.MoveTowardsTarget();
        //Context.Enemy.RotateAroundTarget();

     

        if (Context.Enemy.CanAttackPlayer())
        {
            agent.Stop();
            StateMachine.GoToState("AttackState"); //If within range, try to attack!
        }

        else if(!Context.Enemy.CanSeePlayer())
        {
            agent.Resume();
            StateMachine.GoToState("IdleState"); //If not in vision, return to idle (dont use this if enemy has vision with limited angle)!!
        }
    }
}
