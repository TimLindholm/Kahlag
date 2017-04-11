using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public LayerMask WallMask;		// Player Collision with Camera!
    public float MoveSpeed = 1f;
    public float TurnSmoothing = .5f;
    public float _moveSpeedAdjustment;
    private Transform _camMaster;       // Reach Camera Master obj! (see camera script!)

    // public float TurnSmoothing = 15f;
    private Transform _transform;

    public Vector3 newDir;
    public Vector3 moveVector;

    public bool CanMove = true;
    public bool Moving;

    private Rigidbody _rb;
    private InputScript _ir;
    private float angle;
    public Transform Direction;

    public Vector3 DashDirection;
    public Transform aimDirection;

    //Animation
    private Animator anim;


    private PlayerActionScript _actionRef;


    //Lock-on testing
    public bool UsingLockOn;

    void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
        _camMaster = GameObject.Find("CameraMaster").transform;	// Reach Camera Master obj!	
        _actionRef = GetComponent<PlayerActionScript>();
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
        SetupAnimator();

        anim = GetComponentInChildren<Animator>();
    }
	
	
	void Update ()
    {
        if(_actionRef.InAction == false)
        {
            //MoveAnim();
            MovePlayer();
            MoveSpeedAdjustment();

            if (_ir.RotationX != 0f || _ir.RotationY != 0f)
            {
                Rotate(_ir.RotationX, _ir.RotationY);
            }
            //Move(_ir.MoveAxis);
        }
        

    }

    private void Move(Vector2 dir)
    {

        //newDir = new Vector3(dir.x, 0, dir.y);
        //newDir = Quaternion.Euler(0, _camMaster.eulerAngles.y, 0) * newDir;

        //float animValue = Mathf.Abs(dir.x) + Mathf.Abs(dir.y);
        

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, newDir, out hit, .8f, WallMask))
        {
            if (_actionRef.InAction == false)
            {                       
                    moveVector = newDir * MoveSpeed * Time.deltaTime;
                    _transform.Translate(moveVector, Space.World);
                   
                    Quaternion targetRotation = Quaternion.LookRotation(newDir, Vector3.up);
                    _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, TurnSmoothing * Time.deltaTime);                                     
            }
                     
            
            else
            {
                //anim.SetFloat("Move", 0f);
            }
        }
    }

    void ConvertMoveInputAndPassToAnimator(Vector3 moveInput)
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        float forwardAmount = localMove.z;

        anim.SetFloat("Move", forwardAmount, 0.1f, Time.deltaTime);
    }

    void SetupAnimator()
    {
        anim = GetComponentInChildren<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
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

    private void MovePlayer()
    {
        float animValue = Mathf.Abs(_ir.Horizontal) + Mathf.Abs(_ir.Vertical);

        animValue = Mathf.Clamp01(animValue);
        anim.SetFloat("Move", animValue);

        if (_ir.Horizontal != 0f || _ir.Vertical != 0f)
        {

         
           

            Moving = true;
            //Vector3 newPosition = _rb.position;
            Vector3 newPosition = _transform.position;

            newPosition += new Vector3(_ir.Horizontal * _moveSpeedAdjustment * Time.deltaTime, 0f, 0f);
            newPosition += new Vector3(0f, 0f, _ir.Vertical * _moveSpeedAdjustment * Time.deltaTime);

            //Vector3 walkDirection = (newPosition - _rb.position).normalized;
            Vector3 walkDirection = (newPosition - _transform.position).normalized;
            //Vector3 lookDirection = Direction.forward;  //Ska vara transform.forward på objektet som roterar dit man siktar
            //angle = Vector3.Angle(walkDirection, lookDirection);
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

    public void MoveAnim()
    {
        if(Moving == true)
        {
            anim.SetFloat("Move", 1f);
        }
        else
        {
            anim.SetFloat("Move", 0f);
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
