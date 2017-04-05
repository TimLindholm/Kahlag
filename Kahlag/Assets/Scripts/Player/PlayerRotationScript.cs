using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{

    public float TurnSmoothing;
    public Transform aimDirection;
    private Transform _transform;
    private Rigidbody _rb;
    private InputScript _ir;
  
    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
    }
	
	
	void Update ()
    {
        if (_ir.RotationX != 0f || _ir.RotationY != 0f)
        {
            Rotate(_ir.RotationX, _ir.RotationY);
        }
    }

    private void Rotate(float Horizontal, float Vertical)
    {
        Vector3 targetDirection = new Vector3(Horizontal, 0f, Vertical);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        //Quaternion newRotation = Quaternion.Lerp(_rb.rotation, targetRotation, TurnSmoothing * Time.deltaTime);

        //smooth rotation test
        aimDirection.rotation = targetRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDirection.rotation, TurnSmoothing * Time.deltaTime);

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);


        //_transform.rotation = targetRotation;        
    }
}
