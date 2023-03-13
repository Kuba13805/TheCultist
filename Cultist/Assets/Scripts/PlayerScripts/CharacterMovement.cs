using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GameObject markedDestinationFlag = null;
    public Canvas_ShowHidePanel panel;
    public Animator playerAnimator;
    public bool isIdle;
    public bool isRunning;
    private const float DoubleClickTime = .2f;
    public NavMeshAgent PlayerNavMeshAgent;
    private float lastClickTime;
    private bool doubledClicked;
    public float runningSpeed;
    private float normalSpeed;

    public Camera PlayerCamera;

    private void Start()
    {
        normalSpeed = PlayerNavMeshAgent.speed;
    }

    void Update()
    {
        CheckForDoubleClick();
        IsMoving();
        MovePlayerToPosition();
    }

    private void MovePlayerToPosition()
    {
        if (panel.isActive == false)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit myRaycastHit;
                Ray myRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(myRay, out myRaycastHit))
                {
                    if (isRunning == true)
                    {
                        PlayerNavMeshAgent.speed = runningSpeed;
                        PlayerNavMeshAgent.SetPath(PlayerNavMeshAgent.path);
                        PlayerNavMeshAgent.SetDestination(myRaycastHit.point);
                    }
                    else
                    {
                        PlayerNavMeshAgent.speed = normalSpeed;
                        PlayerNavMeshAgent.SetPath(PlayerNavMeshAgent.path);
                        PlayerNavMeshAgent.SetDestination(myRaycastHit.point);
                    }
                }
            }
        }
    }
    private void IsMoving()
    {
        IsIdle();
        IsRunning();
    }

    private void IsIdle()
    {
        if (PlayerNavMeshAgent.remainingDistance <= PlayerNavMeshAgent.stoppingDistance)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
        
        playerAnimator.SetBool("IsIdle", isIdle);
    }
    
    private void IsRunning()
    {
        if (isIdle == false && doubledClicked == true)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        playerAnimator.SetBool("IsRunning", isRunning);
    }

    private void CheckForDoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            doubledClicked = false;
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= DoubleClickTime)
            {
                doubledClicked = true;
            }
            lastClickTime = Time.time;
        }
    }
}

