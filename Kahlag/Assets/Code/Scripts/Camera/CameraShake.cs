using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance;

    public float _amplitude = 0.1f;

    private Vector3 initialPos;
    public Transform originPos;
    private bool IsShaking;

    void Start()
    {
        Instance = this;
        //initialPos = transform.localPosition;
        initialPos = originPos.localPosition;
        
    }

    public void Shake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        IsShaking = true;
        CancelInvoke();
        Invoke("StopShaking", duration);
    }

    public void StopShaking()
    {
        IsShaking = false;
    }

    void Update()
    {
        if(IsShaking)
        {
            transform.localPosition = initialPos + Random.insideUnitSphere * _amplitude;
        }
    }


}
