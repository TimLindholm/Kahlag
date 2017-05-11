using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public float Damage;
    public float Force;

    public float Distance;

    public float LifeTime = .2f;
    private Collider coll;
    private PlayerHealthScript _playerHealth;

    public bool Collided;

    void Start ()
    {
        _playerHealth = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));
        Killmyself();
        coll = GetComponent<BoxCollider>();

    }

    public void Killmyself()
    {
        //Destroy(gameObject, LifeTime);
    }


    public void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            //Enemy enemyHealth = target.collider.GetComponent<Enemy>();
            //Enemy enemyHealth = GetComponent<Enemy>();
            Enemy enemyHealth = target.GetComponent<Enemy>();
            PlayerHealthScript _playerHealth = target.GetComponent<PlayerHealthScript>();
            print("EnemyHit");

            if (_playerHealth != null)
            {

                //print("Damage");
                _playerHealth.TakeDamage(Damage);
                _playerHealth.KnockBack(-transform.forward * Force);
                Invoke("Trigger", .5f);
                coll.isTrigger = false;               
                //_spawnedBlood = Instantiate(Blood, SpawnPoint.position, SpawnPoint.rotation);
                //_spawnedBlood = Instantiate(Blood, collision.contacts[0].normal, HitRot);

            }
        }
    }

    public void Trigger()
    {
        coll.isTrigger = true;
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    _playerHealth = collision.collider.GetComponent<PlayerHealthScript>();
    //    //HitSound.Play();
    //    if (_playerHealth != null)
    //    {

    //        //print("Damage");
    //        _playerHealth.TakeDamage(Damage);
    //        _playerHealth.KnockBack(-collision.contacts[0].normal * Force);
    //    }
    //}
}
