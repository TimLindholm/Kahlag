using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float MaxHealth = 10;

    public float currentHealth;

    private Enemy _enemyRef;

    public float Damage;

	
	void Start ()
    {
        currentHealth = MaxHealth;
	}
	

	void Update ()
    {
		
	}


    void OnTriggerEnter()
    {
        _enemyRef.TakeDamage(Damage);
    }
}
