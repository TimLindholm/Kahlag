using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    private Rigidbody _rb;

    public bool IsDead;

    public Animator anim;

    public bool Invulnerable;
    public float Invulnerable_Timer = .5f;

    private RagdollManager _rag;
    

    void Start ()
    {
        CurrentHealth = MaxHealth;
        _rb = GetComponent<Rigidbody>();
        SetupAnimator();
        _rag = GetComponent<RagdollManager>();

    }
	
	
	void Update ()
    {
		
	}

    public void KnockBack(Vector3 Force)
    {
        //_rb.AddForce(Force);
    }

    public void TakeDamage(float Damage)
    {
        if(Invulnerable != true)
        {
            
            if (IsDead != true)
            {
                //_score.DamageBonus += Damage;
                anim.SetTrigger("TakeDamage");
                CurrentHealth -= Damage;
                Debug.Log("Enemy Hit!");
                StartCoroutine(InvulnerableTimer());
            }
            if (CurrentHealth <= 0f && IsDead == false)
            {
                //_rb.constraints = RigidbodyConstraints.None;
                //StaminaRef._stamina = StaminaRef.Stamina;
                _rag.RagdollCharacter();
                _rag.CloseAllComponents();
                IsDead = true;
            }
        }

    }

    IEnumerator InvulnerableTimer()
    {
        Invulnerable = true;
        yield return new WaitForSeconds(Invulnerable_Timer);
        Invulnerable = false;
    }

    void SetupAnimator()
    {
        anim = GetComponentInChildren<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
}
