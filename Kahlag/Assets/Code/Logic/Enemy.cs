using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform m_target;
	public float m_viewRange;
    public float m_attackRage;

    public float randomDir;
    private float _right = .05f;
    private float _left = -.05f;

    public float ActionTimer;

    public Transform aimDirection;
    public float VisionRadius = 30f;
    public float TurnSmoothing = 5f;

    public Animator anim;


    //Health Related
    public float Health;
    public bool IsDead;

    Rigidbody m_body;

    //Navmesh Testing
    UnityEngine.AI.NavMeshAgent agent;

    public GameObject[] waypoints; //for patrolling, if enemy does not have any - won't patrol
    int currentWP = 0;
    public float accuracyWP;

    private void Awake()
	{
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing       
        agent.autoBraking = false; //does not slow down 

        SetupAnimator(); //get anim component
        m_body = GetComponent<Rigidbody>();
	}

    void Update()
    {
        ActionCooldown();
    }
    public void ActionCooldown()
    {
        if(ActionTimer >= 0f)
        {
            ActionTimer -= Time.deltaTime;           
        }
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, m_viewRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRage);
	}

	public bool CanSeePlayer()
	{
        //return Vector3.Distance(transform.position, m_target.position) < m_viewRange;

        Vector3 targetDir = m_target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);       
        return Vector3.Distance(m_target.position, transform.position) < m_viewRange && (angle < VisionRadius);
    }

    public bool CanAttackPlayer()
    {
        return Vector3.Distance(transform.position, m_target.position) < m_attackRage;
    }

	public void MoveTowardsTarget()
	{
		//m_body.AddForce((m_target.position - m_body.position).normalized * 25f);

        //NAVMESH TEST

        agent.SetDestination(m_target.position);
        Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);
        anim.SetFloat("Moving", 1.1f);
    }

    public void RotateAroundTarget()
    {
        aimDirection.LookAt(m_target);
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing * Time.deltaTime);
        transform.Translate(randomDir, 0f, 0f * Time.deltaTime /100); // Fix this!
    }

	public void MoveAwayFromTarget()
	{
		m_body.AddForce((m_body.position - m_target.position).normalized * 25f);
	}

	public void MoveTowardsPosition(Vector3 pos)
	{
        //m_body.AddForce((pos - m_body.position).normalized * 13f);

        //NAVMESH TEST
        // Returns if no points have been set up
        //Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);
        //anim.SetFloat("Moving", relDirection.z, 2f, Time.deltaTime);
        anim.SetFloat("Moving", 0.6f);
        if (waypoints.Length == 0)
        {
            anim.SetFloat("Moving", 0f);
            return;
            
        }
        
        agent.destination = waypoints[currentWP].transform.position;
       
        currentWP = (currentWP + 1) % waypoints.Length;
    }


    public void RandomizeRotation()
    {
        randomDir = Random.Range(_left, _right);

    }

    
    
    
    
    
    
    
    //Take Damage Related
    public void KnockBack(Vector3 Force)
    {
        m_body.AddForce(Force);
    }

    public void TakeDamage(float Damage)
    {
        if (IsDead != true)
        {
            //_score.DamageBonus += Damage;
            anim.SetTrigger("TakeDamage");
            Health -= Damage;
            Debug.Log("Enemy Hit!");
        }
        if (Health <= 0f && IsDead == false)
        {
            m_body.constraints = RigidbodyConstraints.None;
            //StaminaRef._stamina = StaminaRef.Stamina;
            
            IsDead = true;
        }
    }



    //Anim Related
    void SetupAnimator()
    {
        anim = GetComponentInChildren<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if(childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
}
