﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMaster : MonoBehaviour
{

    //Camera
    public Transform gameplayPosition;
    public Transform menuPosition;
    public Transform gameCamera;
    public FadeScript fadeScript;

    public GameObject TitleText;

    public bool GameStarted;
    public bool PressedStart;

    private InputScript _ir;
	

	void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        if(GameStarted==false && PressedStart==false)
        {
            gameCamera.position = menuPosition.position;
            gameCamera.rotation = menuPosition.rotation;
            print("Started");
        }
   
        //Invoke("FadeIn", 2f);
	}
	
	
	void Update ()
    {
        if(PressedStart == false && GameStarted == false)
        {
            if (_ir.StartGame == true)
            {
                StartCoroutine(StartTheGame());
                PressedStart = true;
            }
        }
	}

    public void StartTheLevel()
    {

    }

    public void FadeIn()
    {
        fadeScript.ShouldFading = true;
    }

    public void FadeOut()
    {
        fadeScript.ShouldFading = false;
    }

    public void StartGame()
    {

    }

    IEnumerator StartTheGame()
    {
        fadeScript.ShouldFading = false;
        yield return new WaitForSeconds(2f);
        TitleText.SetActive(false);
        gameCamera.position = gameplayPosition.position;
        gameCamera.rotation = gameplayPosition.rotation;
        yield return new WaitForSeconds(2f);
        fadeScript.ShouldFading = true;
        
        GameStarted = true;
    }

    public void SetCameraPos()
    {
        gameCamera.position = gameplayPosition.position;
        gameCamera.rotation = gameplayPosition.rotation;
    }
}
