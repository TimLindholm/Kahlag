using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{

    public bool PressedPlay;
    public bool PressedQuit;

    public Button PlayButton;
    public Button QuitButton;

    public AudioSource PressedAudio;

    public float Timer = 3f;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(PressedPlay == true)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0f)
            {
                SceneManager.LoadScene(1);
            }
        }

        if (PressedQuit == true)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                Application.Quit();
            }

        }
    }

    public void Play()
    {
        PressedPlay = true;



        //SpriteState PlayButtonState = PlayButton.spriteState;
        //PlayButtonState.disabledSprite = PlayButtonState.pressedSprite;
        //PlayButton.spriteState = PlayButtonState;
        //PlayButton.interactable = false;
        //QuitButton.interactable = false;
    }

    public void Quit()
    {
        PressedQuit = true;
      
        PressedAudio.Play();
        SpriteState QuitButtonState = QuitButton.spriteState;
        QuitButtonState.disabledSprite = QuitButtonState.pressedSprite;
        QuitButton.spriteState = QuitButtonState;
        PlayButton.interactable = false;
        QuitButton.interactable = false;
    }
}
