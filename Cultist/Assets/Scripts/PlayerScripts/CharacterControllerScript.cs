using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
{
    public GameObject markedDestinationFlag;
    private bool hasSpawnedFlag = false;
    GameObject flag;

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



    private void Awake()
    {
        InputManager.Instance.PlayerInputActions.Player.MoveCharacter.performed += MovePlayerToPosition;
    }

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        PlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        normalSpeed = PlayerNavMeshAgent.speed;
    }

    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.Player.MoveCharacter.performed -= MovePlayerToPosition;
    }

    private void Update()
    {
        DestroyFlagWhenDestinationReached();
        IsMoving();
    }
    private void Interact(BaseInteractableObject interactionToPerformOnObject)
    {
        if (PlayerNavMeshAgent.remainingDistance != 0 || (!(Vector3.Distance(PlayerNavMeshAgent.transform.position,
                interactionToPerformOnObject.interactor.interactorPosition) < 1.0))) return;
        
        PlayerNavMeshAgent.transform.LookAt(interactionToPerform.transform);
        interactionToPerformOnObject.Interact();
    }
    private void MovePlayerToPosition(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        
        Ray myRay = PlayerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (!Physics.Raycast(myRay, out var myRaycastHit)) return;

        if (myRaycastHit.transform.CompareTag("Floor"))
        {
            if (CheckForPath(myRaycastHit.point) == false) return;
            
            SpawnFlagAtDestination(myRaycastHit.point);
            
            DetermineMovement(myRaycastHit.point, transform.position);
        }
        else
        {
            MoveToInteract(myRaycastHit);
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

        if (!interactorFound) return;
        
        if (Vector3.Distance(PlayerNavMeshAgent.transform.position, newPosition) > 1.0f)
        {
            if (CheckForPath(newPosition) == false) return;
            
            SpawnFlagAtDestination(newPosition);
            
            DetermineMovement(PlayerNavMeshAgent.transform.position, interactionToPerform.interactor.interactorPosition, newPosition);
        }
        else
        {
            Interact(interactionToPerform);
        }
    }
    private void DetermineMovement(Vector3 startPoint, Vector3 endPoint)
    {
        if (Vector3.Distance(startPoint,endPoint) >= 5f)
        {
            isRunning = true;
            playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(runningSpeed, startPoint);
        }
        else
        {
            isRunning = false;
            playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(normalSpeed, startPoint);
        }
    }
    private void DetermineMovement(Vector3 startPoint, Vector3 endPoint, Vector3 positionToMove)
    {
        if (Vector3.Distance(startPoint,endPoint) >= 5f)
        {
            isRunning = true;
            playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(runningSpeed, positionToMove);
        }
        else
        {
            isRunning = false;
            playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(normalSpeed, positionToMove);
        }
    }
    private void IsMoving()
    {
        if (PlayerNavMeshAgent.remainingDistance <= PlayerNavMeshAgent.stoppingDistance)
        {
            isIdle = true;
            isRunning = false;
        }
        else
        {
            isIdle = false;
        }
        playerAnimator.SetBool("IsRunning", isRunning);
        playerAnimator.SetBool("IsIdle", isIdle);
    }

    private void ChangeMovement(float speed, Vector3 position)
    {
        PlayerNavMeshAgent.speed = speed;
        PlayerNavMeshAgent.SetPath(PlayerNavMeshAgent.path);
        PlayerNavMeshAgent.SetDestination(position);
    }

    private void SpawnFlagAtDestination(Vector3 hitPosition)
    {
        if (flag != null)
        {
            Destroy(flag);
            hasSpawnedFlag = false;
        }

        if (hasSpawnedFlag || Time.timeScale == 0) return;
        
        float objectHeight = markedDestinationFlag.GetComponent<Renderer>().bounds.size.y / 2f;
        
        var spawnPoint = new Vector3(hitPosition.x, hitPosition.y + objectHeight, hitPosition.z);
        
        flag = Instantiate(markedDestinationFlag, spawnPoint, Quaternion.identity);
        
        hasSpawnedFlag = true;

    }

    private void DestroyFlagWhenDestinationReached()
    {
        if (!hasSpawnedFlag || PlayerNavMeshAgent.remainingDistance != 0) return;
        
        Destroy(flag);
        hasSpawnedFlag = false;
    }

    private bool CheckForPath(Vector3 pointToCalculatePath)
    {
        NavMeshPath path = new NavMeshPath();

        PlayerNavMeshAgent.CalculatePath(pointToCalculatePath, path);

        return path.status == NavMeshPathStatus.PathComplete;
    }
}

