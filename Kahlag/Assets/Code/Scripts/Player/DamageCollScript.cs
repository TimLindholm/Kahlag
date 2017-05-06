using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollScript : MonoBehaviour
{
    public float Damage;
    public float Force;
    public Transform target;


    //public float LifeTime = .2f;

    public AudioSource HitSound;
    private Transform _transform;

    public float Reach;
    public float Distance;
    private Transform enemyToDamage;
    private Enemy _enemyHealth;
    public GameObject Blood;
    private GameObject _spawnedBlood;
    public Transform SpawnPoint;


    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        //DealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Reach);
    }

    public void DealDamage()
    {
        Ray enemyCheck = new Ray(transform.position, transform.forward / Distance);
        Debug.DrawRay(transform.position, transform.forward * Distance);
        RaycastHit hit;

        //if (Physics.SphereCast(transform.position, Reach, transform.forward, out hit) && hit.transform.tag == "Enemy")
        if (Physics.SphereCast(enemyCheck, Reach, out hit) && hit.transform.tag == "Enemy")
        {
            //enemyToDamage = GameObject.FindGameObjectWithTag("Enemy");
            enemyToDamage = hit.transform;
            _enemyHealth = enemyToDamage.GetComponent<Enemy>();

            print("Damage");
            _enemyHealth.TakeDamage(Damage);

            //enemyToDamage = null;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemyHealth = collision.collider.GetComponent<Enemy>();
        //HitSound.Play();

        Vector3 collisionHitRot = collision.contacts[0].normal;
        Quaternion HitRot = Quaternion.LookRotation(Vector3.forward, collisionHitRot);

        _spawnedBlood = Instantiate(Blood, SpawnPoint.position, SpawnPoint.rotation);

        if (enemyHealth != null)
        {

            //print("Damage");
            enemyHealth.TakeDamage(Damage);
            enemyHealth.KnockBack(-collision.contacts[0].normal * Force);

           
        }
    }

}
