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

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _rb = GetComponent<Rigidbody>();
    }
	
	
	void Update ()
    {
        PlayerDash();

    }

    private void PlayerDash()
    {
        if (MovementRef.Moving == true && _ir.Dash == true && StaminaRef.CurrentStamina >= DashCost)
        {
            ApplyForce();
            StaminaRef.CurrentStamina -= DashCost;

            Debug.Log("Dashing");
        }
    }
    private void ApplyForce()
    {
        {
            _rb.AddForce(MovementRef.DashDirection * DashSpeed, ForceMode.Impulse);
            //Debug.Log("ApplyingForce");
        }
    }
}
