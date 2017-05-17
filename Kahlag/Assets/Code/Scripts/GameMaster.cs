using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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


    public GameObject bossPercussion;

    public GameObject PlayerUI;

    public PlayerHealthScript healthRef;

    public GameObject GameOverText;
    private Color White;

    public bool PlayerHasFailed;
	

	void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));

   
          //bossPercussion.SetActive(false);
        //bossPercussion.SetActive(true);
        if (GameStarted==false && PressedStart==false)
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

        if(healthRef.IsDead == true && PlayerHasFailed == false)
        {
            StartCoroutine(GameOver());
            PlayerHasFailed = true;
        }
	}

    public void StartTheLevel()
    {

    }


    public void ReloadScene()
    {
       
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        //Fade in GameOverText
        GameOverText.SetActive(true);
        yield return new WaitForSeconds(2f);     
        FadeOut();
        yield return new WaitForSeconds(5f);
        //Fade out GameOverText
        GameOverText.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);

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
        PlayerUI.SetActive(true);
    }

    public void SetCameraPos()
    {
        gameCamera.position = gameplayPosition.position;
        gameCamera.rotation = gameplayPosition.rotation;
    }
}
