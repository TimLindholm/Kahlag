using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public GameObject LightAttack;
    public GameObject HeavyAttack;

    public float LightDamage = 5f;
    public float HeavyDamage = 12f;
	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}

    public void DealDamage()
    {

    }

    public void OnCollisionEnter()
    {
        //EnemyHealthScript enemyHealth = collision.collider.GetComponent<EnemyHealthScript>(); // Get the enemyHealthScript
        
        //enemyHealth.TakeDamage(Damage); //Reach the "TakeDamage"Function in the EnemyHealthScript & send an Amount of Damage too it
        
        //enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
    }
}
