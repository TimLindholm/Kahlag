using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedStrike : MonoBehaviour
{
    private InputScript _ir;
    private Animator anim;
    private PlayerActionScript _actionRef;
    private DetectEnemyScript _detectRef;

    public GameObject ChargeVFX;

    public float ChargeTime;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _actionRef = GetComponent<PlayerActionScript>();
        anim = GetComponentInChildren<Animator>();
        _detectRef = GetComponent<DetectEnemyScript>();
    }
	
	
	void Update ()
    {
		
	}
}
