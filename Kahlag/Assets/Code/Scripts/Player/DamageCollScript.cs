using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollScript : MonoBehaviour
{
    public float Damage;
    public float Force;
    public Transform target;

    public float Distance = 5f;

    //public float LifeTime = .2f;

    public AudioSource HitSound;

    private EnemyHealthScript _enemyHealth;

    private Transform _transform;

    public float Reach;


    void Start ()
    {
        _transform = transform;
        
        

    }
	
	
	void Update ()
    {
        //DealDamage();
        //CheckIfActive();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Reach);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Reach);
    }

    //public bool DamageEnemy()
    //{
    //    //return Vector3.Distance(transform.position, m_target.position) < m_viewRange;
    //    Vector3 targetDir = target.position - transform.position;
       

    //    return Vector3.Distance(target.transform.position, transform.position) < Reach;
    //}

    //public void Killmyself()
    //{
        
      
    //}



    //private void DealDamage()
    //{
    //    Ray enemyCheck = new Ray(transform.position, transform.forward * Distance);
    //    Debug.DrawRay(transform.position, transform.forward * Distance);
    //    RaycastHit hit;

    //    if (Physics.Raycast(enemyCheck, out hit) && hit.transform.tag == "Enemy")
    //    {
    //        Enemy enemyHealth = hit.transform.GetComponent<Enemy>();
    //        //HitSound.Play();
    //        if (enemyHealth != null)
    //        {

    //            print("Damage");
    //            enemyHealth.TakeDamage(Damage);
    //            //enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider coll)
    //{
    //    Enemy enemyHealth = GetComponent<Enemy>();
    //    //Debug.Log("damage");
    //    //HitSound.Play();
    //    if (enemyHealth != null)
    //    {

    //        print("Damage");
    //        enemyHealth.TakeDamage(Damage);
    //        //enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemyHealth = collision.collider.GetComponent<Enemy>();
        //HitSound.Play();
        if (enemyHealth != null)
        {

            //print("Damage");
            enemyHealth.TakeDamage(Damage);
            enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
        }
    }
}
