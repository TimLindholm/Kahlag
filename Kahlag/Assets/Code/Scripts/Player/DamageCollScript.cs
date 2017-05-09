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

    public void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Enemy")
        {
            //Enemy enemyHealth = target.collider.GetComponent<Enemy>();
            //Enemy enemyHealth = GetComponent<Enemy>();
            Enemy enemyHealth = target.GetComponent<Enemy>();
            
            print("EnemyHit");
            
            if (enemyHealth != null)
            {

                //print("Damage");
                enemyHealth.TakeDamage(Damage);
                Debug.Log("Damage");
                enemyHealth.KnockBack(-transform.forward * Force);

                _spawnedBlood = Instantiate(Blood, SpawnPoint.position, SpawnPoint.rotation);
                //_spawnedBlood = Instantiate(Blood, collision.contacts[0].normal, HitRot);

            }
        }
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    Enemy enemyHealth = collision.collider.GetComponent<Enemy>();
    //    //HitSound.Play();

    //    Vector3 collisionHitRot = collision.contacts[0].normal;
    //    Quaternion HitRot = Quaternion.LookRotation(Vector3.forward, collisionHitRot);

    //    _spawnedBlood = Instantiate(Blood, SpawnPoint.position, SpawnPoint.rotation);
    //    //_spawnedBlood = Instantiate(Blood, collision.contacts[0].normal, HitRot);

    //    if (enemyHealth != null)
    //    {

    //        //print("Damage");
    //        enemyHealth.TakeDamage(Damage);
    //        enemyHealth.KnockBack(-collision.contacts[0].normal * Force);

           
    //    }
    //}

}
