using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    private Rigidbody _rb;

    public bool IsDead;

    public Animator anim;

    public bool Invulnerable;
    public float Invulnerable_Timer = .5f;

    public bool Damaged;

    private RagdollManager _rag;
    private PlayerActionScript _actionRef;

    //UI
    public Slider HealthSlider;
    public Image Fill;

    public Color colorStart;
    public Color colorEnd;


    void Start ()
    {
        CurrentHealth = MaxHealth;
        _rb = GetComponent<Rigidbody>();
        _actionRef = GetComponent<PlayerActionScript>();
        SetupAnimator();
        _rag = GetComponent<RagdollManager>();

        colorStart = Color.black;
        colorEnd = Color.red;
    }
	
	
	void Update ()
    {
		if(Damaged == true)
        {
            Invoke("IfDamaged", .5f);
           
        }

        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        HealthSlider.value = CurrentHealth;
        Fill.color = Color.Lerp(colorStart, colorEnd, Mathf.InverseLerp(0, MaxHealth, CurrentHealth));
    }

    void IfDamaged()
    {         
            Damaged = false;
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
                CancelInvoke("IfDamaged");
                Damaged = true;
                CurrentHealth -= Damage;
                //Debug.Log("Player Hit!");
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
