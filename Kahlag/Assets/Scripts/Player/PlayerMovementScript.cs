using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float _moveSpeedAdjustment;

   // public float TurnSmoothing = 15f;
    private Transform _transform;

    public bool CanMove = true;
    public bool Moving;

    private Rigidbody _rb;
    private InputScript _ir;
    private float angle;
    public Transform Direction;

    public Vector3 DashDirection;


    //Lock-on testing
    public bool UsingLockOn;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _transform = transform;
        _rb = GetComponent<Rigidbody>();   
    }
	
	
	void Update ()
    {
        MovePlayer();
        MoveSpeedAdjustment();
    }

    private void MovePlayer()
    {
        if (_ir.Horizontal != 0f || _ir.Vertical != 0f)
        {
            Moving = true;
            //Vector3 newPosition = _rb.position;
            Vector3 newPosition = _transform.position;

            newPosition += new Vector3(_ir.Horizontal * _moveSpeedAdjustment * Time.deltaTime, 0f, 0f);
            newPosition += new Vector3(0f, 0f, _ir.Vertical * _moveSpeedAdjustment * Time.deltaTime);

            //Vector3 walkDirection = (newPosition - _rb.position).normalized;
            Vector3 walkDirection = (newPosition - _transform.position).normalized;
            Vector3 lookDirection = Direction.forward;  //Ska vara transform.forward på objektet som roterar dit man siktar
            angle = Vector3.Angle(walkDirection, lookDirection);
            //Debug.Log(angle);

            DashDirection = walkDirection;
            //_rb.MovePosition(newPosition);
            //_transform.Translate(newPosition);
            _transform.position = Vector3.Lerp(_transform.position, newPosition, _moveSpeedAdjustment * Time.deltaTime);

        }
        else
        {
            Moving = false;
        }
    }

    private void MoveSpeedAdjustment()
    {
        if(UsingLockOn == true)
        {
            _moveSpeedAdjustment = MoveSpeed * 1.2f;
        }
        else if(UsingLockOn == false)
        {
            if (angle <= 60f)
            {
                _moveSpeedAdjustment = MoveSpeed * 1.2f;

            }
            else if (angle > 135f)           //Add else infront to use above segment
            {
                _moveSpeedAdjustment = MoveSpeed * 0.8f;
            }
            else
            {
                _moveSpeedAdjustment = MoveSpeed;
            }
        }

    }


}
