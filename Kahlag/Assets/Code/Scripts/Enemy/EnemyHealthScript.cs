using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float MaxHealth = 10;

    public float currentHealth;

	
	void Start ()
    {
        currentHealth = MaxHealth;
	}
	

	void Update ()
    {
		
	}


    void OnTriggerEnter()
    {

    }
}
