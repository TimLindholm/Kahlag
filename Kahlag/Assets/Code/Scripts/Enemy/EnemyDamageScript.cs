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

    public bool BruteCollider;

    void Start ()
    {
        _playerHealth = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));
        coll = GetComponent<BoxCollider>();

    }


    public void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            //Enemy enemyHealth = target.GetComponent<Enemy>();
            PlayerHealthScript _playerHealth = target.GetComponent<PlayerHealthScript>();
            print("EnemyHit");

            if (_playerHealth != null)
            {

                //print("Damage");
                _playerHealth.TakeDamage(Damage);
                _playerHealth.KnockBack(-transform.forward * Force);
                Invoke("Trigger", .5f);
                coll.isTrigger = false;               
            }
        }

        if(BruteCollider == true)
        {
            if (target.tag == "Enemy")
            {       
                var enemyHealth = target.GetComponent<Enemy>();
                if(enemyHealth != null)
                {
                    if(enemyHealth.IsBrute == false)
                    {
                        enemyHealth.TakeDamage(10);
                    }             
                }           
            }
        }

    }

    public void Trigger()
    {
        coll.isTrigger = true;
    }
}
