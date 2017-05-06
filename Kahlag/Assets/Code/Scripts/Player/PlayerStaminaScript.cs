using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaScript : MonoBehaviour
{
    public float MaximumStamina = 10f;
    public float CurrentStamina;

    public float RecoveryRate = 5f;

    // UI
    public Slider StaminaSlider;
    public Image Fill;

    public Color colorStart;
    public Color colorEnd;

    void Start ()
    {
        colorStart = Color.black;
        colorEnd = Color.cyan;
        CurrentStamina = MaximumStamina;
    }
	
	
	void Update ()
    {
        RecoverStamina();
        UpdateStaminaBar();
    }

    private void RecoverStamina()
    {
        if (CurrentStamina < MaximumStamina)
        {
            CurrentStamina += Time.deltaTime * RecoveryRate;
        }
    }

    private void UpdateStaminaBar()
    {
        StaminaSlider.value = CurrentStamina;
        Fill.color = Color.Lerp(colorStart, colorEnd, Mathf.InverseLerp(0, MaximumStamina, CurrentStamina));
    }

    public void FillStamina()
    {
        CurrentStamina = MaximumStamina;
    }
}
