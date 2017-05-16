using System.Collections;
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
	

	void Start ()
    {
        gameCamera.position = menuPosition.position;
        gameCamera.rotation = menuPosition.rotation;
	}
	
	
	void Update ()
    {
		
	}

    public void StartGame()
    {
        //Start the game (press play)
        //fade out
        //TitleText.SetActive(false);
        //Move Camera to gamepos
        //fade in
    }

    public void SetCameraPos()
    {
        gameCamera.position = gameplayPosition.position;
        gameCamera.rotation = gameplayPosition.rotation;
    }
}
