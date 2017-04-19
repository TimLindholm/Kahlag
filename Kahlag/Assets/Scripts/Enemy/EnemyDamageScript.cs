using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public float Damage;
    public float Force;

    private PlayerHealthScript _playerHealth;

    void Start ()
    {
        _playerHealth = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));

    }
	
	
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        _playerHealth = collision.collider.GetComponent<PlayerHealthScript>();
        //HitSound.Play();
        if (_playerHealth != null)
        {

            print("Damage");
            _playerHealth.TakeDamage(Damage);
            _playerHealth.KnockBack(-collision.contacts[0].normal * Force);
        }
    }
}
