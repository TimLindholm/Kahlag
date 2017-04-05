using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{

    public float Horizontal;
    public float Vertical;

    public float RotationX;
    public float RotationY;

    public bool Dash;

    public bool LockOn;

    //public Vector2 MoveAxis;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        RotationX = Input.GetAxis("RotationX");
        RotationY = Input.GetAxis("RotationY");

        Dash = Input.GetKeyDown(KeyCode.Joystick1Button5);
        LockOn = Input.GetKeyDown(KeyCode.Joystick1Button4);
    }
}
