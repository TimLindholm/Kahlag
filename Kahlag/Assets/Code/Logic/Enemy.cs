using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform m_target;
	public float m_viewRange;
    public float m_attackRage;

    public float randomDir;
    public float _right = .05f;
    public float _left = -.05f;

    public float ActionTimer;

    public float ComboCooldown;

    public Transform aimDirection;
    public float VisionRadius = 30f;
    public float TurnSmoothing = 5f;

    public Animator anim;



    //Control Damage Animation
    public bool HasDamageAnim = true;


    //Camera Shake
    public bool Cam_Shake = true;
    public float amplitude = 0.1f;
    public float duration = 0.5f;

    //Health Related
    public float Health;
    public bool IsDead;

    public bool invulnerable;
    public bool inAttack;

    private RagdollEnemy _rag;

    Rigidbody m_body;


    //Random Attack
    public int randomAttack;
    private int horizontal = 0;
    private int vertical = 3;


    //Navmesh Testing
    UnityEngine.AI.NavMeshAgent agent;

    public GameObject[] waypoints; //for patrolling, if enemy does not have any - won't patrol
    int currentWP = 0;
    public float accuracyWP;

    private void Awake()
	{
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing       
        agent.autoBraking = false; //does not slow down 
        _rag = GetComponent<RagdollEnemy>();

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

        if(ComboCooldown >= 0f)
        {
            ComboCooldown -= Time.deltaTime;
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
            //anim.SetFloat("Moving", 1.1f);
            anim.SetFloat("Movement", relDirection.z, .5f, Time.deltaTime);
            anim.SetBool("Strafing", false);
        
    
    }

    public void RotateAroundTarget()
    {
        if(invulnerable != true)
        {
            if(randomDir > 0)
            {
                randomDir = .03f;
            }
            else
            {
                randomDir = -.03f;
            }
            aimDirection.LookAt(m_target);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing * Time.deltaTime);
            transform.Translate(randomDir, 0f, 0f * Time.deltaTime / 200); // Fix this!
            anim.SetBool("Strafing", true);

        }
        //else
        //{
        //    anim.SetBool("Strafing", false);
        //}


        if(randomDir >=0)
        {
            //anim.SetTrigger("strafeRight");
            anim.SetFloat("Strafe", 1);
        }
        else
        {
            //anim.SetTrigger("strafeLeft");*
            anim.SetFloat("Strafe", -1);
        }

    }

    public void TakeAim()
    {
        aimDirection.LookAt(m_target);
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing * Time.deltaTime);
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


        anim.SetFloat("Movement", 1f);
        //anim.SetFloat("Moving", 0.6f);
        if (waypoints.Length == 0)
        {
            //anim.SetFloat("Moving", 0f);
            anim.SetFloat("Movement", 0f);
            return;
            
        }

       
        
        agent.destination = waypoints[currentWP].transform.position;
       
        currentWP = (currentWP + 1) % waypoints.Length;
    }


    public void RandomizeRotation()
    {
        randomDir = Random.Range(_left, _right);

    }

    public void RandomizeAttack()
    {
        randomAttack = Random.Range(horizontal, vertical);
        //print(randomAttack);
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
            

            if(invulnerable != true)
            {
                //_score.DamageBonus += Damage;
                if(inAttack != true)
                {
                    if(HasDamageAnim==true)
                    {
                        anim.SetTrigger("TakeDamage");
                    }
                   
                }

                agent.Stop();
                Health -= Damage;
                if(Cam_Shake==true)
                {
                    CameraShake.Instance.Shake(amplitude, duration);
                }
                
                Debug.Log("Enemy Hit!");
                Invoke("InvulnerableTimer", .2f);
                invulnerable = true;
            }

            

        }
        if (Health <= 0f && IsDead == false)
        {
            
            //StaminaRef._stamina = StaminaRef.Stamina;
            
            IsDead = true;
            m_body.isKinematic = true;
            m_body.constraints = RigidbodyConstraints.None;
            _rag.RagdollCharacter();
            _rag.CloseAllComponents();
            Debug.Log("ENEMY DEAD!");
        }
    }

    public void InvulnerableTimer()
    {
        //agent.Resume();
        invulnerable = false;
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
