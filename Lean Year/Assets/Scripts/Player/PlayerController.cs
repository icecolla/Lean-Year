using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    private float distanceTarget;

    private Quaternion playerRot;
    public float rotSpeed = 5;

    public bool isMoving = false;
    public bool isRunning = false;

    public float walkSpeed = 2f;
    public float runSpeed = 6f;

    [Range(0, 1)]
    public float turnSmoothTime = 0.2f;

    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

    float animationSpeedPercent = 0f;

    private Animator animator;

    public int floorMask;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }
        else if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Action on the objects");
        }
        if (isMoving)
            Move();
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, floorMask))
        {
            targetPosition = hit.point;
            distanceTarget = Vector3.Distance(this.transform.position, targetPosition);

            if ( distanceTarget > 1.5f)
            {
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y,
                                           targetPosition.z - transform.position.z);
                playerRot = Quaternion.LookRotation(lookAtTarget);
                isMoving = true;
            }
        }    
    }

    private void Move()
    {
        float targetSpeed = ((isRunning) ? runSpeed : walkSpeed);

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        SetAnimation();

        if (Vector3.Distance(transform.position,targetPosition) < 0.01f)
        {
            isMoving = false;
            isRunning = false;
        }
    }

    private void SetAnimation()
    {
        if (isMoving)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);

            animationSpeedPercent = ((isRunning) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
            animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }
    }
}
