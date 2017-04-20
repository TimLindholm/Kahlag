using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollScript : MonoBehaviour
{
    public float Damage;
    public float Force;

    public float Distance = 5f;

    public AudioSource HitSound;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        DealDamage();

    }

    private void DealDamage()
    {
        Ray enemyCheck = new Ray(transform.position, transform.forward * Distance);
        Debug.DrawRay(transform.position, transform.forward * Distance);
        RaycastHit hit;

        if (Physics.Raycast(enemyCheck, out hit) && hit.transform.tag == "Enemy")
        {
            Enemy enemyHealth = hit.transform.GetComponent<Enemy>();
            //HitSound.Play();
            if (enemyHealth != null)
            {

                print("Damage");
                enemyHealth.TakeDamage(Damage);
                //enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Enemy enemyHealth = collision.collider.GetComponent<Enemy>();
    //    //HitSound.Play();
    //    if (enemyHealth != null)
    //    {

    //        print("Damage");
    //        enemyHealth.TakeDamage(Damage);
    //        enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
    //    }
    //}
}
