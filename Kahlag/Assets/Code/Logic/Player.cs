using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float m_moveForce = 5f;
	Rigidbody m_body;

	void Start ()
	{
		m_body = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.W))
			m_body.AddForce(m_moveForce * Vector3.forward);
		if (Input.GetKey(KeyCode.A))
			m_body.AddForce(m_moveForce * Vector3.left);
		if (Input.GetKey(KeyCode.S))
			m_body.AddForce(m_moveForce * Vector3.back);
		if (Input.GetKey(KeyCode.D))
			m_body.AddForce(m_moveForce * Vector3.right);
	}
}
