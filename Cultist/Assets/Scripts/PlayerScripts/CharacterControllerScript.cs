using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
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

    private BaseInteractableObject interactionToPerform;
    
    Vector3 newPosition;

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
        if (Time.timeScale != 0)
        {
            RaycastHit myRaycastHit;
            Ray myRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Input.GetMouseButton(0))
            {

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

    private void Interact(BaseInteractableObject interactionToPerform)
    {
        if (PlayerNavMeshAgent.remainingDistance == 0 && (Vector3.Distance(PlayerNavMeshAgent.transform.position, interactionToPerform.interactor.interactorPosition) < 1.0))
        {
            interactionToPerform.Interact();
        }
    }
    private void MoveToInteract(RaycastHit myRaycastHit)
    {
        GameObject gameObjectToInteract = myRaycastHit.transform.gameObject;
        newPosition = transform.position;
        bool interactorFound;
        
        try
        {
            newPosition = gameObjectToInteract.GetComponentInChildren<InteractorScript>().interactorPosition;
            interactionToPerform = gameObjectToInteract.GetComponent<BaseInteractableObject>();
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
                if (isIdle)
                {
                    Interact(interactionToPerform);
                }
                //transform.LookAt(gameObjectToInteract.transform);
            }
            else
            {
                ChangeMovement(normalSpeed, newPosition);
                if (isIdle)
                {
                    Interact(interactionToPerform);
                }
                //transform.LookAt(gameObjectToInteract.transform);
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
        if (!hasSpawnedFlag && Time.timeScale != 0)
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

