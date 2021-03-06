﻿using System.Collections;
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
    public bool HeavyAttack;

    public float HeavyAttackFloat;

    public Vector2 MoveAxis;

    private PlayerHealthScript _healthRef;

    public bool RestartLevel;

    public bool StartGame;

    public GameMaster gameMaster;

    //public Vector2 MoveAxis;

    void Start ()
    {
        _healthRef = (PlayerHealthScript)FindObjectOfType(typeof(PlayerHealthScript));
	}
	
	
	void Update ()
    {
        if(_healthRef.IsDead == false)
        {
            if(gameMaster.GameStarted == true)
            {
                MoveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                Horizontal = Input.GetAxis("Horizontal");
                Vertical = Input.GetAxis("Vertical");

                RotationX = Input.GetAxis("RotationX");
                RotationY = Input.GetAxis("RotationY");

                HeavyAttackFloat = Input.GetAxis("HeavyAttack");

                

                Dash = Input.GetKeyDown(KeyCode.Joystick1Button1);
                //LockOn = Input.GetKeyDown(KeyCode.Joystick1Button4);

                FastAttack = Input.GetKeyDown(KeyCode.JoystickButton5);
                HeavyAttack = Input.GetKeyDown(KeyCode.JoystickButton4);

            }
        }

        if(gameMaster.GameStarted == false)
        {
            StartGame = Input.GetKeyDown(KeyCode.Joystick1Button7);
        }
  
        RestartLevel = Input.GetKeyDown(KeyCode.JoystickButton6);


    }
}
