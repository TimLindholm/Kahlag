using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{

    //Idle State
    //Vision <- Alert nearby allies before entering Combat State!
    //Alerted -> Go to Combat State if Alerted

    //Combat State
    //Loop Actions, if unable to perform an Action, move towards the player!
    //Action Cooldown - Behaviour when waiting for the Action Cooldown?

    //If Enemy is Damaged and NOT in CombatState, enters Combat State after x seconds (probably .5 - 1.5)
    UnityEngine.AI.NavMeshAgent agent;
    public Transform Player;
    public Transform Head;
    private Transform _transform;
    private Animator anim;


    //public float rotSpeed;
    //public float moveSpeed;




    //vision
    public float VisionRadius = 30f;

    public bool InCombatState;

    //Enemy Actions
    public bool InAction; // True during Action
    public float ActionTimer;
    private float _actionTimer;



    
    public GameObject[] waypoints; //for patrolling, if enemy does not have any - won't patrol
    int currentWP = 0;
    public float accuracyWP;
   
	
	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();     
        _transform = transform;
        //SetupAnimator();
        //anim = GetComponent<Animator>();

        Patrol();
        agent.autoBraking = false; //does not slow down 
    }




    void Update ()
    {
        Vision();
        CombatState();
        ActionCooldown();
        if (InCombatState == true)
        {
            //MoveTowardsPlayer();
        }
        //MoveTowardsPlayer();
        if (agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
            
    }

    private void Patrol()
    {
        if(InCombatState == false)
        {
            // Returns if no points have been set up
            if (waypoints.Length == 0)
            {
                return;
            }         
            agent.destination = waypoints[currentWP].transform.position;
            currentWP = (currentWP + 1) % waypoints.Length;
        }     
    }


    public void CombatState() //Try to execute Actions, if unable, Move towards Player!
    {
        //Check Actions - Requirements! - If Able, Execute Action!
        if(InCombatState == true)
        {
            if (Vector3.Distance(Player.position, _transform.position) < 1)
            {
                
                // Try to execute Actions here!
                if(InAction == false)
                {
                    // Random Int Actions, Int 1 = SingleAttack, Int 2 = FlurryAttack, Int3 = Delay/Dodge
                    //StopCoroutine(MoveTowardsPlayer());
                    StartCoroutine(SingleAttack(transform));
                }
            }
            else if(InAction == false)
            {
                MoveTowardsPlayer();
                //StartCoroutine(MoveTowardsPlayer());

            }
        }      
    }

    public void Vision()
    {
        //if(InCombatState == true)
        {
            //VISION - Look for Player
            //If player is Spotted, Alert Allies -> Combat State!
            Vector3 direction = Player.position - _transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(direction, Head.up);

            if (Vector3.Distance(Player.position, _transform.position) < 10 && (angle < VisionRadius))
            {
                //Start Alert Coroutine (includes InCombat == true)
                if (InCombatState != true)
                {
                    Debug.Log("I See You!");
                    //Do Alert Coroutine!
                    InCombatState = true;
                }
               
            }
        }
    }

    //public IEnumerator MoveTowardsPlayer()
    //{
    //    yield return new WaitForSeconds(.1f);
    //    agent.SetDestination(Player.position);
    //    Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);
    //    print("Chasing Player");
    //}



    private void MoveTowardsPlayer()
    {
        //if (InCombatState == true)
        {
            agent.SetDestination(Player.position);
            Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);

            //anim.SetFloat("moving", relDirection.z, 1f, Time.deltaTime);
            print("Chasing Player");
        }
    }


    public void ActionCooldown() //Action Control
    {
        if (InAction == true)
        {
            if (ActionTimer >= _actionTimer)
            {
                ActionTimer -= Time.deltaTime;
            }
            else if(ActionTimer <= _actionTimer)
            {
                InAction = false;
            }
        }
    }

    public IEnumerator SingleAttack(Transform transform)
    {
        ActionTimer = 5f;
        InAction = true;
        print("Attacking");
        yield return new WaitForSeconds(.5f);
        
    }

    public IEnumerator FlurryAttack(Transform transform)
    {
        ActionTimer = 5f;
        InAction = true;
        yield return new WaitForSeconds(.5f);
        
    }




    //void SetupAnimator()
    //{
    //    anim = GetComponent<Animator>();

    //    foreach (var childAnimator in GetComponentsInChildren<Animator>())
    //    {
    //        if (childAnimator != anim)
    //        {
    //            anim.avatar = childAnimator.avatar;
    //            Destroy(childAnimator);
    //            break;
    //        }
    //    }
    //}

}
