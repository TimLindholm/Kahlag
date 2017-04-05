using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaScript : MonoBehaviour
{
    public float MaximumStamina = 10f;
    public float CurrentStamina;

    public float RecoveryRate = 5f;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        RecoverStamina();
    }

    private void RecoverStamina()
    {
        if (CurrentStamina < MaximumStamina)
        {
            CurrentStamina += Time.deltaTime * RecoveryRate;
        }
    }
}
