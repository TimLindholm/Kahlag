using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollScript : MonoBehaviour
{
    public float Damage;
    public float Force;


    public AudioSource HitSound;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemyHealth = collision.collider.GetComponent<Enemy>();
        //HitSound.Play();
        if (enemyHealth != null)
        {

            
            enemyHealth.TakeDamage(Damage);
            enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
        }
    }
}
