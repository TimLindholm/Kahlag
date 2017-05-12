using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashScript : MonoBehaviour
{
    public PlayerMovementScript MovementRef;
    public PlayerStaminaScript StaminaRef;

    public float DashCost;

    public float DashSpeed = 5f;
    public float DashCooldown;

    private Rigidbody _rb;
    private InputScript _ir;

    public Transform aimDir;

    private PlayerActionScript _actionRef;

    private Animator anim;


    public bool SetPlayerInActionDuringDash;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        _rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
	
	
	void Update ()
    {
        PlayerDash();

    }

    private void PlayerDash()
    {
        if(SetPlayerInActionDuringDash == true)
        {
            if (_ir.Dash == true && StaminaRef.CurrentStamina >= DashCost && _actionRef.InAction == false)
            {
                ApplyForce();
                StaminaRef.CurrentStamina -= DashCost;
                StartCoroutine(DashActionTimer());
                //Debug.Log("Dashing");
            }
        }
        else
        {
            if (_ir.Dash == true && StaminaRef.CurrentStamina >= DashCost)
            {
                ApplyForce();
                StaminaRef.CurrentStamina -= DashCost;
                //StartCoroutine(DashActionTimer());
                //Debug.Log("Dashing");
            }
        }


    }
    private void ApplyForce()
    {
        {
            transform.rotation = aimDir.rotation;
            anim.SetTrigger("Dash");
            //_rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
            
            //transform.Translate(0f, 0f, DashSpeed * Time.deltaTime /5);


            //Debug.Log("ApplyingForce");
        }
    }

    IEnumerator DashActionTimer()
    {
        _actionRef.InAction = true;
        yield return new WaitForSeconds(.3f);
        _actionRef.InAction = false;
    }
}
