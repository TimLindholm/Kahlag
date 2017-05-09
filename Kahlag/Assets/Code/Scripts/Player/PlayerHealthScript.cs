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

    //Health Recovery
    public Slider HealthRecoveryBar;
    public Image recoveryFill;
    public float HealthRecovery = 0;
    private int _currentHealthRecovery;

    public float DecayRate;
    public float  DecayTimer = 3;
    public float  _timer;

    public int StaminaLossWhenHit = 3;

    //Stamina ref
    private PlayerStaminaScript _stamRef;
    
    



    void Start ()
    {
        CurrentHealth = MaxHealth;
        _rb = GetComponent<Rigidbody>();
        _actionRef = GetComponent<PlayerActionScript>();
        SetupAnimator();
        _rag = GetComponent<RagdollManager>();
        _stamRef = GetComponent<PlayerStaminaScript>();

        colorStart = Color.black;
        colorEnd = Color.red;
    }
	
	
	void Update ()
    {
		if(Damaged == true)
        {
            Invoke("IfDamaged", .5f);           
        }

        UpdateRecoveryBar();
        UpdateHealthBar();
        GainHealth();
        DecayOverTime();

    }

    public void GainHealth()
    {
        if(HealthRecovery >= 10 && CurrentHealth < MaxHealth)
        {
            CurrentHealth += 1;
            HealthRecovery = 0;
        }
    }

    public void DecayOverTime()
    {
        if(HealthRecovery >= 0)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;

            }

            if (_timer <= 0)
            {
                HealthRecovery -= DecayRate * Time.deltaTime;
            }
        }

    }



    private void UpdateHealthBar()
    {
        HealthSlider.value = CurrentHealth;
        Fill.color = Color.Lerp(colorStart, colorEnd, Mathf.InverseLerp(0, MaxHealth, CurrentHealth));
    }

    private void UpdateRecoveryBar()
    {
        HealthRecoveryBar.value = HealthRecovery;
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
                _stamRef.CurrentStamina -= StaminaLossWhenHit; //REDUCE STAMINA WHEN HIT
                if(_stamRef.CurrentStamina < 0)
                {
                    _stamRef.CurrentStamina = 0;
                }
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
