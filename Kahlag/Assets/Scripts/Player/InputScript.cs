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

    public bool FastAttack;

    public Vector2 MoveAxis;


    //public Vector2 MoveAxis;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {

        MoveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        RotationX = Input.GetAxis("RotationX");
        RotationY = Input.GetAxis("RotationY");

        

        Dash = Input.GetKeyDown(KeyCode.Joystick1Button5);
        //LockOn = Input.GetKeyDown(KeyCode.Joystick1Button4);

        FastAttack = Input.GetKeyDown(KeyCode.JoystickButton0);
    }
}
