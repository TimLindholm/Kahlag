using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFacingCameraScript : MonoBehaviour
{

    public Camera main_camera;

    private CameraShake _camRef;

	void Start ()
    {
        //_camRef = (CameraShake)FindObjectOfType(typeof(CameraShake));
        //main_camera = _camRef.
	}
	
	
	void Update ()
    {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.back, main_camera.transform.rotation * Vector3.up);	
	}
}
