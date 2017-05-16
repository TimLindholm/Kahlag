using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float FadeTime = 1f;
    public GameObject Screen;
    public Image ScreenImage;
    private float timer;

    private Color Black;

    public bool ShouldFading;

    public float InactiveTimer;
    private float _inactiveTimer;

    void Start()
    {
        Black = ScreenImage.color;
        _inactiveTimer = InactiveTimer;
    }


    void Update()
    {
        if (ShouldFading == true)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }

    }

    public void FadeIn()
    {
        _inactiveTimer -= Time.deltaTime;

        timer += Time.deltaTime / FadeTime;
        timer = Mathf.Clamp01(timer);
        Black.a = Mathf.Lerp(1, 0, timer);
        ScreenImage.color = Black;
        if (_inactiveTimer <= 0f)
        {
            Screen.SetActive(false);
        }
        //print("fadin");
    }

    public void FadeOut()
    {
        Screen.SetActive(true);
        timer -= Time.deltaTime / FadeTime;
        timer = Mathf.Clamp01(timer);
        Black.a = Mathf.Lerp(1, 0, timer);
        ScreenImage.color = Black;
        //print("fadeout");
    }
}
