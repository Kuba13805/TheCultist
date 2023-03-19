using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public GameObject markedDestinationFlag;
    private bool hasSpawnedFlag = false;
    GameObject flag;
    
    public Canvas_ShowHidePanel panel;
    
    private Animator playerAnimator;
    
    public bool isIdle;
    public bool isRunning;
    
    private NavMeshAgent PlayerNavMeshAgent;
    
    private const float DoubleClickTime = .2f;
    private float lastClickTime;
    private bool doubledClicked;
    
    public float runningSpeed;
    private float normalSpeed;

    public Camera PlayerCamera;


    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        PlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        normalSpeed = PlayerNavMeshAgent.speed;
    }

    void Update()
    {
        DestroyFlagWhenDestinationReached();
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
                    if (myRaycastHit.transform.CompareTag("Floor"))
                    {
                        SpawnFlagAtDestination(myRaycastHit.point);
                        if (isRunning)
                        {
                            ChangeMovement(runningSpeed, myRaycastHit.point);
                        }
                        else
                        {
                            ChangeMovement(normalSpeed, myRaycastHit.point);
                        }
                    }
                    else
                    {
                        MoveToInteract(myRaycastHit);
                    }
                }
            }
        }
    }

    private void MoveToInteract(RaycastHit myRaycastHit)
    {
        GameObject gameObjectToInteract = myRaycastHit.transform.gameObject;
        Vector3 newPosition = this.transform.position;
        bool interactorFound;
        
        try
        {
            newPosition = gameObjectToInteract.GetComponentInChildren<InteractorScript>().interactorPosition;

            interactorFound = true;
        }
        catch (Exception e)
        {
            interactorFound = false;
        }

        if (interactorFound)
        {
            SpawnFlagAtDestination(newPosition);
            if (isRunning)
            {
                ChangeMovement(runningSpeed, newPosition);
                transform.LookAt(gameObjectToInteract.transform);
            }
            else
            {
                ChangeMovement(normalSpeed, newPosition);
                transform.LookAt(gameObjectToInteract.transform);
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
        if (isIdle == false && (doubledClicked || PlayerNavMeshAgent.remainingDistance >= 10f))
        {
            if (PlayerNavMeshAgent.remainingDistance >= 10f)
            {
                doubledClicked = true;
            }
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        playerAnimator.SetBool("IsRunning", isRunning);
    }

    private void ChangeMovement(float speed, Vector3 position)
    {
        PlayerNavMeshAgent.speed = speed;
        PlayerNavMeshAgent.SetPath(PlayerNavMeshAgent.path);
        PlayerNavMeshAgent.SetDestination(position);
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

    private void SpawnFlagAtDestination(Vector3 hitPosition)
    {
        
        Vector3 spawnPoint;
        if (flag != null)
        {
            Destroy(flag);
            hasSpawnedFlag = false;
        }
        if (!hasSpawnedFlag)
        {
            float objectHeight = markedDestinationFlag.GetComponent<Renderer>().bounds.size.y / 2f;
            spawnPoint = new Vector3(hitPosition.x, hitPosition.y + objectHeight, hitPosition.z);
            flag = Instantiate(markedDestinationFlag, spawnPoint, Quaternion.identity);
            hasSpawnedFlag = true;
        }
        
    }

    private void DestroyFlagWhenDestinationReached()
    {
        if (hasSpawnedFlag && PlayerNavMeshAgent.remainingDistance == 0)
        {
            Destroy(flag);
            hasSpawnedFlag = false;
        }
    }
}

