using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Enemy : MonoBehaviour
{
	public Transform m_target;
	public float m_viewRange;
    public float m_attackRage;

    public float randomDir;
    public float _right = .05f;
    public float _left = -.05f;

    public float ActionTimer;

    public float ComboCooldown;

    public float ChargeCooldown;

    public Transform aimDirection;
    public float VisionRadius = 30f;
    public float TurnSmoothing = 5f;

    public Animator anim;



    //Control Damage Animation
    public bool HasDamageAnim = true;


    //Camera Shake
    public bool Cam_Shake = true;
    public float amplitude = 0.1f;
    public float duration = 0.5f;

    //Health Related
    public float CurrentHealth;
    public float MaxHealth;
    public bool IsDead;

    public bool invulnerable;
    public bool inAttack;

    //UI health
    public Slider HealthSlider;
    public Image Fill;
    public GameObject EnemyHealth;

    public GameObject hitParticle;
    private GameObject _spawnedParticle;
    public Transform particleSpawnPoint;


    //check if cultist or brute
    public bool IsCultist;
    public bool IsBrute;

    private RagdollEnemy _rag;

    Rigidbody m_body;


    //Random Attack
    public int randomAttack;

    private int downswing = 0;
    private int meleeswing = 2;

    private int horizontal = 0;
    private int vertical = 3;


    public bool EnteredCombat;
    private PlayerStaminaScript _staminaRef;
    private PlayerHealthScript _healthRef;

    //When the enemy dies, should it restore stamina to the player?
    public bool FillStaminaOnDeath; // <----
    public bool FillHealthOnHit;


    public bool TakingDamage;

    //Find player
    private PlayerHealthScript _playerRef;

    //Alert allies
    public float AlertRadius = 6f;
    public float AlertTimer = 2f;
    public bool IsAlerted;

    //Navmesh Testing
    UnityEngine.AI.NavMeshAgent agent;


    //GameMasterRef
    private GameMaster _gm;


    public GameObject[] waypoints; //for patrolling, if enemy does not have any - won't patrol
    int currentWP = 0;
    public float accuracyWP;

    [FMODUnity.EventRef]
    public string CultistAttackEvent;
    FMOD.Studio.EventInstance Cultist_Attack;

    [FMODUnity.EventRef]
    public string CultistTakeDamageEvent;
    FMOD.Studio.EventInstance Cultist_Impacts;

    [FMODUnity.EventRef]
    public string BossTakeDamageEvent;
    FMOD.Studio.EventInstance Boss_Impacts;

    [FMODUnity.EventRef]
    public string CultistFootEvent;
    FMOD.Studio.EventInstance Cultist_Footsteps;

    [FMODUnity.EventRef]
    public string BossDeathEvent;
    FMOD.Studio.EventInstance Growl_Death;

    [FMODUnity.EventRef]
    public string CultistDeathEvent;
    FMOD.Studio.EventInstance Cultist_Death;

    private void Awake()
	{
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Navmesh Testing       
        _staminaRef = (PlayerStaminaScript)FindObjectOfType(typeof(PlayerStaminaScript));
        _healthRef = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));
        agent.autoBraking = false; //does not slow down 
        _rag = GetComponent<RagdollEnemy>();
        CurrentHealth = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = MaxHealth;
        SetupAnimator(); //get anim component
        m_body = GetComponent<Rigidbody>();

        _gm = (GameMaster)FindObjectOfType(typeof(GameMaster));
        _playerRef = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));
        m_target = _playerRef.transform;

        //Hide UI when at full health
        EnemyHealth.SetActive(false);


    }

    void Cultist_Swing()
    {
        FMODUnity.RuntimeManager.PlayOneShot(CultistAttackEvent,GetComponent<Transform>().position);
    }

    void Cultist_Foot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(CultistFootEvent,GetComponent<Transform>().position);
    }
    
    
    //void CultistImpact()
    // {
    //   Cultist_Impacts = FMODUnity.RuntimeManager.CreateInstance(CultistTakeDamageEvent);
    //   Cultist_Impacts.start();
    //}

    void Update()
    {
        ActionCooldown();
        UpdateHealthBar();
    }

    public void ActionCooldown()
    {
        if(ActionTimer >= 0f)
        {
            ActionTimer -= Time.deltaTime;           
        }

        if(ComboCooldown >= 0f)
        {
            if(EnteredCombat)
            ComboCooldown -= Time.deltaTime;
        }

        if(ChargeCooldown >= 0f)
        {
            if(EnteredCombat)
            ChargeCooldown -= Time.deltaTime;
        }
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, m_viewRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRage);
	}

	public bool CanSeePlayer()
	{
        //return Vector3.Distance(transform.position, m_target.position) < m_viewRange;
        Vector3 targetDir = m_target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);       
        return Vector3.Distance(m_target.position, transform.position) < m_viewRange && (angle < VisionRadius);
    }

    public bool CanAttackPlayer()
    {
        return Vector3.Distance(transform.position, m_target.position) < m_attackRage;
    }

	public void MoveTowardsTarget()
	{
		//m_body.AddForce((m_target.position - m_body.position).normalized * 25f);

        //NAVMESH TEST
       
            agent.SetDestination(m_target.position);
            Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity);
            //anim.SetFloat("Moving", 1.1f);
            anim.SetFloat("Movement", relDirection.z, .5f, Time.deltaTime);
            anim.SetBool("Strafing", false);
        
    
    }

    public void RotateAroundTarget()
    {
        if(invulnerable != true)
        {
            if(randomDir > 0)
            {
                randomDir = .015f;
            }
            else
            {
                randomDir = -.015f;
            }

            if(TakingDamage != true)
            {
                aimDirection.LookAt(m_target);
                transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing * Time.deltaTime);
                transform.Translate(randomDir, 0f, 0f * Time.deltaTime); // Fix this!
                anim.SetBool("Strafing", true);
            }          
        }
        //else
        //{
        //    anim.SetBool("Strafing", false);
        //}


        if(randomDir >=0)
        {
            //anim.SetTrigger("strafeRight");
            anim.SetFloat("Strafe", 1);
        }
        else
        {
            //anim.SetTrigger("strafeLeft");*
            anim.SetFloat("Strafe", -1);
        }

    }

    public void TakeAim()
    {
        aimDirection.LookAt(m_target);
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing /2 * Time.deltaTime);
    }

	public void MoveAwayFromTarget()
	{
		m_body.AddForce((m_body.position - m_target.position).normalized * 25f);
	}

	public void MoveTowardsPosition(Vector3 pos)
	{
  
        if(waypoints.Length > 0)
        {
            anim.SetFloat("Movement", 1f);
            //anim.SetFloat("Moving", 0.6f);
            if (waypoints.Length == 0)
            {
                //anim.SetFloat("Moving", 0f);
                anim.SetFloat("Movement", 0f);
                return;

            }
            agent.destination = waypoints[currentWP].transform.position;
            currentWP = (currentWP + 1) % waypoints.Length;
        }
       
    }

    public void RandomizeRotation()
    {
        randomDir = Random.Range(_left, _right);

    }

    public void RandomizeCultistAttack()
    {
        randomAttack = Random.Range(downswing, meleeswing);
    }

    public void RandomizeBruteAttack()
    {
        randomAttack = Random.Range(horizontal, vertical);
        //print(randomAttack);
    }

    private void UpdateHealthBar()
    {
        HealthSlider.value = CurrentHealth;
        //Fill.color = Color.Lerp(colorStart, colorEnd, Mathf.InverseLerp(0, MaximumStamina, CurrentStamina));
    }

    public void KnockBack(Vector3 Force)
    {
   
        m_body.AddForce(Force);
    }

    public void Alert()
    {
        var colliders = Physics.OverlapSphere(transform.position, AlertRadius);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Enemy")
            {              
                var enemy = collider.GetComponent<Enemy>();
                enemy.Invoke("IamAlerted", AlertTimer);                                 
            }
        }
    }

    public void TakeDamage(float Damage)
    {
        if (IsDead != true)
        {
            EnemyHealth.SetActive(true); // activate healthbar
            if(invulnerable != true)
            {
                if(IsCultist == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(CultistTakeDamageEvent, GetComponent<Transform>().position);
                }

                if (IsBrute == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(BossTakeDamageEvent, GetComponent<Transform>().position);
                }


                if (HasDamageAnim == true)
                {
                    TakingDamage = true;
                    if (Damage > 2)
                    {
                       
                        agent.isStopped = true;
                        Invoke("StartMovingAgain", 1.3f);
                        anim.SetTrigger("TakeHeavyDamage");
                        ActionTimer = 1.5f;
                        print("heavyDamage");
                        m_body.velocity = Vector3.zero;
                        m_body.angularVelocity = Vector3.zero;
                        
                    }

                    if (inAttack != true)
                    {
                        if(HasDamageAnim==true)
                        {
                            anim.SetTrigger("TakeDamage");
                            m_body.velocity = Vector3.zero;
                            m_body.angularVelocity = Vector3.zero;
                        }
                    }          
                }

                if (FillHealthOnHit == true) // <---- Get health when damaging
                {
                    if (_healthRef.CurrentHealth < _healthRef.MaxHealth)
                    {
                        if(Damage > 3)
                        {
                            _healthRef.HealthRecovery += 3.5f;
                            _healthRef._timer = _healthRef.DecayTimer;
                        }
                     
                        else
                        {
                            _healthRef.HealthRecovery += 1.5f;
                            _healthRef._timer = _healthRef.DecayTimer;
                        }
                    }
                }

                Invoke("StartRotatingAgain", 1f);

                
                agent.isStopped = true;
                CurrentHealth -= Damage;
                if(IsCultist == true)
                {
                    _spawnedParticle = Instantiate(hitParticle, particleSpawnPoint.position, particleSpawnPoint.rotation);
                }
               

                if (Cam_Shake==true)
                {
                    CameraShake.Instance.Shake(amplitude, duration);                  
                }
                

                Debug.Log("Enemy Hit!");
                Invoke("InvulnerableTimer", .2f);
                invulnerable = true;
            }
        }
        if (CurrentHealth <= 0f && IsDead == false)
        {

            //StaminaRef._stamina = StaminaRef.Stamina;
            if(FillStaminaOnDeath == true)
            {
                _staminaRef.FillStamina();
            }
   
            IsDead = true;

            _gm.EnemiesSlain += 1;

            if(IsBrute==true)
            {
                FMODUnity.RuntimeManager.PlayOneShot(BossDeathEvent, GetComponent<Transform>().position);
            }

            if (IsCultist == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot(CultistDeathEvent, GetComponent<Transform>().position);
            }

            m_body.isKinematic = true;
            m_body.constraints = RigidbodyConstraints.None;
            var force = -(aimDirection.rotation * Vector3.forward) * 10 + Vector3.up * 3; 
            _rag.RagdollCharacter(force);
            _rag.CloseAllComponents();
            Debug.Log("ENEMY DEAD!");
        }
    }

    public void InvulnerableTimer()
    {
        //agent.Resume();
        m_body.isKinematic = false;
        invulnerable = false;
    }

    public void IamAlerted()
    {
        IsAlerted = true;
    }

    void StartMovingAgain()//Invoked
    {    
        agent.isStopped = false;
    } 

    void StartRotatingAgain()
    {
        TakingDamage = false;
    } //Invoked

    void SetupAnimator()
    {
        anim = GetComponentInChildren<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if(childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
}
