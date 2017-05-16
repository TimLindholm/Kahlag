using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : StateBehaviour
{

    //Navmesh Testing
    UnityEngine.AI.NavMeshAgent agent;

    public bool CanCharge;

    public override void OnEnter()
    {
        //Debug.Log("Enter Combat State");
        agent.speed = 4;
    
        agent.isStopped = false;
        Context.Enemy.EnteredCombat = true;

        Context.Enemy.Alert(); // Alerts nearby allies

    }

    public override void OnExit()
    {
        agent.isStopped = true;
        agent.speed = 3;
    }

    public void Awake()
    {
        
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing    
    }

    void Update()
    {
        
        if(CanCharge == true)
        {
            //if (Context.Enemy.ChargeCooldown <= 0f && Context.Enemy.inAttack != true) 
            if (Context.Enemy.ChargeCooldown <= 0f) 
            {
                StateMachine.GoToState("ChargeState");
            }
        }
      

        Context.Enemy.MoveTowardsTarget();
        //Context.Enemy.RotateAroundTarget();

     

        if (Context.Enemy.CanAttackPlayer())
        {
           
            agent.isStopped = true;
            StateMachine.GoToState("AttackState"); 
        }

        //TAKE BACK TO LEAVE COMBAT AND GO BACK TO IDLE
        //else if(!Context.Enemy.CanSeePlayer())
        //{
        //    agent.Resume();
        //    StateMachine.GoToState("IdleState"); //If not in vision, return to idle (dont use this if enemy has vision with limited angle)!!
        //}
    }
}
