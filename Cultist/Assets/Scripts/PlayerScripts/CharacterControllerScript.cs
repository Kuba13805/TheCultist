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
        InputManager.Instance.ChangeActionMapToPlayer();
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

    void Update()
    {
        DestroyFlagWhenDestinationReached();
        IsMoving();
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
            SpawnFlagAtDestination(myRaycastHit.point);
            if (Vector3.Distance(myRaycastHit.point, transform.position) >= 5f)
            {
                isRunning = true;
                playerAnimator.SetBool("IsRunning", isRunning);
                ChangeMovement(runningSpeed, myRaycastHit.point);
            }
            else
            {
                isRunning = false;
                playerAnimator.SetBool("IsRunning", isRunning);
                ChangeMovement(normalSpeed, myRaycastHit.point);
            }
        }
        else
        {
            MoveToInteract(myRaycastHit);
        }
    }
    
    private void Interact(BaseInteractableObject interactionToPerform)
    {
        if (PlayerNavMeshAgent.remainingDistance == 0 && (Vector3.Distance(PlayerNavMeshAgent.transform.position, interactionToPerform.interactor.interactorPosition) < 1.0))
        {
            PlayerNavMeshAgent.transform.LookAt(this.interactionToPerform.transform);
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

        if (!interactorFound) return;
        if (Vector3.Distance(PlayerNavMeshAgent.transform.position, interactionToPerform.interactor.interactorPosition) > 1.0f)
        {
            SpawnFlagAtDestination(newPosition);
            if (Vector3.Distance(PlayerNavMeshAgent.transform.position, interactionToPerform.interactor.interactorPosition) >= 5f)
            {
                isRunning = true;
                playerAnimator.SetBool("IsRunning", isRunning);
                ChangeMovement(runningSpeed, newPosition);
            }
            else
            {
                isRunning = false;
                playerAnimator.SetBool("IsRunning", isRunning);
                ChangeMovement(normalSpeed, newPosition);
            }
        }
        else
        {
            Interact(interactionToPerform);
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
        if (!hasSpawnedFlag && Time.timeScale != 0)
        {
            float objectHeight = markedDestinationFlag.GetComponent<Renderer>().bounds.size.y / 2f;
            var spawnPoint = new Vector3(hitPosition.x, hitPosition.y + objectHeight, hitPosition.z);
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

