using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(CanvasGroup))]
public class UISceneScript : MonoBehaviour
{
    public float FadeInTime = 0.5f;
    public float FadeOutTime = 0.5f;
    public float FadeInDelay = 0.5f;
    public float FadeOutDelay = 0.5f;

    public bool FadeOutOnStart;
    public bool StartHidden = true;

    private CanvasGroup _canvasGroup;


    private float _delayTimer, _lerpTimer, _lerpTargetTime, _startAlpha, _targetAlpha, _fadeTime, _queueDelayTimer, _maxFadeTime;

   

	void Start ()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if(StartHidden == true)
        {
            Hide();
        }
        else
        {
            Show();
        }

        if(FadeOutOnStart == true)
        {
            FadeOutUI();
        }
	}
	
	
	void Update ()
    {
        if(_lerpTimer < _lerpTargetTime)
        {
            _lerpTimer += Time.deltaTime / _fadeTime;
            _canvasGroup.alpha = Mathf.Lerp(_startAlpha, _targetAlpha, _lerpTimer / _lerpTargetTime);
        }
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0f;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1f;
    }

    public void FadeInUI()
    {
        _startAlpha = _canvasGroup.alpha;
        _lerpTargetTime = 1f;
        _lerpTimer = 0f;
        _targetAlpha = 1f;
        _fadeTime = FadeInTime;
    }

    public void FadeOutUI()
    {
        _startAlpha = _canvasGroup.alpha;
        _lerpTargetTime = 1f;
        _lerpTimer = 0f;
        _targetAlpha = 0f;
        _fadeTime = FadeOutTime;
    }

    public void FadeOutScreen()
    {

    }
    
    public void FadeInScreen()
    {

    }
}
