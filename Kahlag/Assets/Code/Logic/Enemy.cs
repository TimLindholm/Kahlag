using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform m_target;
	public float m_viewRange;
    public float m_attackRage;

    public float ActionTimer;

    public Transform aimDirection;
    public float TurnSmoothing = 5f;
    public float Health;

    Rigidbody m_body;

    //Navmesh Testing
    //UnityEngine.AI.NavMeshAgent agent;

    private void Awake()
	{
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing
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
		return Vector3.Distance(transform.position, m_target.position) < m_viewRange;
	}

    public bool CanAttackPlayer()
    {
        return Vector3.Distance(transform.position, m_target.position) < m_attackRage;
    }

	public void MoveTowardsTarget()
	{
		m_body.AddForce((m_target.position - m_body.position).normalized * 25f);
	}

    public void RotateAroundTarget()
    {
        aimDirection.LookAt(m_target);
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing * Time.deltaTime);
        transform.Translate(Vector3.right * Time.deltaTime); // Fix this!
    }

	public void MoveAwayFromTarget()
	{
		m_body.AddForce((m_body.position - m_target.position).normalized * 25f);
	}

	public void MoveTowardsPosition(Vector3 pos)
	{
		m_body.AddForce((pos - m_body.position).normalized * 13f);
	}
}
