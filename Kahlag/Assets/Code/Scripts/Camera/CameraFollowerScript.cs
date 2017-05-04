using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerScript : MonoBehaviour
{
    public Transform Target;
    public float dampTime = 0.15f;

    private Vector3 delta;
    private Transform _trans;
    private Vector3 velocity = Vector3.zero;

    void Start ()
    {
        delta = transform.position - Target.position;
        _trans = transform;
    }
	//Increase FoV when enemies are nearby?
	
	void Update ()
    {
        FollowPlayer();

    }

    private void FollowPlayer()
    {
        if (Target)
        {
            Vector3 destination = Target.position + delta;
            _trans.position = Vector3.SmoothDamp(_trans.position, destination, ref velocity, dampTime);

            //TEST
            //_trans.rotation = Quaternion.Lerp(_trans.rotation, Target.rotation, rotationDampTime * Time.deltaTime);
        }
    }


}
