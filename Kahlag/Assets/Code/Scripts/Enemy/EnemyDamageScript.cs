using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public float Damage;
    public float Force;

    public float Distance;

    public float LifeTime = .2f;

    private PlayerHealthScript _playerHealth;

    void Start ()
    {
        _playerHealth = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));
        Killmyself();

    }

    public void Killmyself()
    {
        //Destroy(gameObject, LifeTime);
    }


    void Update ()
    {
		//DealDamage();
	}

    //private void DealDamage()
    //{
    //    Ray enemyCheck = new Ray(transform.position, transform.forward * Distance);
    //    Debug.DrawRay(transform.position, transform.forward * Distance);
    //    RaycastHit hit;

    //    if (Physics.Raycast(enemyCheck, out hit) && hit.transform.tag == "Player")
    //    {
    //        _playerHealth = hit.transform.GetComponent<PlayerHealthScript>();
    //        //HitSound.Play();
    //        if (_playerHealth != null)
    //        {

    //            print("Damage");
    //            _playerHealth.TakeDamage(Damage);
    //            //enemyHealth.KnockBack(-collision.contacts[0].normal * Force);
    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        _playerHealth = collision.collider.GetComponent<PlayerHealthScript>();
        //HitSound.Play();
        if (_playerHealth != null)
        {

            //print("Damage");
            _playerHealth.TakeDamage(Damage);
            _playerHealth.KnockBack(-collision.contacts[0].normal * Force);
        }
    }
}
