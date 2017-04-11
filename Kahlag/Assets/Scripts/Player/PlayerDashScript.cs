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

    private PlayerActionScript _actionRef;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        _rb = GetComponent<Rigidbody>();
    }
	
	
	void Update ()
    {
        PlayerDash();

    }

    private void PlayerDash()
    {
        if (_ir.Dash == true && StaminaRef.CurrentStamina >= DashCost && _actionRef.InAction == false)
        {
            ApplyForce();
            StaminaRef.CurrentStamina -= DashCost;
            StartCoroutine(DashActionTimer());
            Debug.Log("Dashing");
        }
    }
    private void ApplyForce()
    {
        {
            _rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
            //Debug.Log("ApplyingForce");
        }
    }

    IEnumerator DashActionTimer()
    {
        _actionRef.InAction = true;
        yield return new WaitForSeconds(.5f);
        _actionRef.InAction = false;
    }
}
