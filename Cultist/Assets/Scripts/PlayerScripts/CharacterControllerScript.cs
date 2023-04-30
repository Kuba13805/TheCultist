using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterControllerScript : MonoBehaviour
{
    private GameObject markedDestinationFlag;
    private bool _hasSpawnedFlag = false;
    private ParticleSystem _flag;
    [SerializeField] private ParticleSystem clickEffect;

    private Animator _playerAnimator;
    
    public bool isIdle;
    public bool isRunning;

    private NavMeshAgent _playerNavMeshAgent;
    
    private const float DoubleClickTime = .2f;
    private float _lastClickTime;
    private bool _doubledClicked;
    
    public float runningSpeed;
    private float _normalSpeed;

    [FormerlySerializedAs("PlayerCamera")] public Camera playerCamera;

    private BaseInteractableObject _interactionToPerform;

    private Vector3 _newPosition;



    private void Awake()
    {
        InputManager.Instance.PlayerInputActions.Player.MoveCharacter.performed += MovePlayerToPosition;
    }

    private void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerNavMeshAgent = GetComponent<NavMeshAgent>();
        _normalSpeed = _playerNavMeshAgent.speed;
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
        if (_playerNavMeshAgent.remainingDistance != 0 || (!(Vector3.Distance(_playerNavMeshAgent.transform.position,
                interactionToPerformOnObject.interactor.interactorPosition) < 1.0))) return;
        
        _playerNavMeshAgent.transform.LookAt(_interactionToPerform.transform);
        interactionToPerformOnObject.Interact();
    }
    private void MovePlayerToPosition(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        
        var myRay = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
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
        var gameObjectToInteract = myRaycastHit.transform.gameObject;
        _newPosition = transform.position;
        bool interactorFound;
        
        try
        {
            _newPosition = gameObjectToInteract.GetComponentInChildren<InteractorScript>().interactorPosition;
            _interactionToPerform = gameObjectToInteract.GetComponent<BaseInteractableObject>();
            interactorFound = true;
        }
        catch (Exception e)
        {
            interactorFound = false;
        }

        if (!interactorFound) return;
        
        if (Vector3.Distance(_playerNavMeshAgent.transform.position, _newPosition) > 1.0f)
        {
            if (CheckForPath(_newPosition) == false) return;
            
            SpawnFlagAtDestination(_newPosition);
            
            DetermineMovement(_playerNavMeshAgent.transform.position, _interactionToPerform.interactor.interactorPosition, _newPosition);
        }
        else
        {
            Interact(_interactionToPerform);
        }
    }
    private void DetermineMovement(Vector3 startPoint, Vector3 endPoint)
    {
        if (Vector3.Distance(startPoint,endPoint) >= 5f)
        {
            isRunning = true;
            _playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(runningSpeed, startPoint);
        }
        else
        {
            isRunning = false;
            _playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(_normalSpeed, startPoint);
        }
    }
    private void DetermineMovement(Vector3 startPoint, Vector3 endPoint, Vector3 positionToMove)
    {
        if (Vector3.Distance(startPoint,endPoint) >= 5f)
        {
            isRunning = true;
            _playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(runningSpeed, positionToMove);
        }
        else
        {
            isRunning = false;
            _playerAnimator.SetBool("IsRunning", isRunning);
            ChangeMovement(_normalSpeed, positionToMove);
        }
    }
    private void IsMoving()
    {
        if (_playerNavMeshAgent.remainingDistance <= _playerNavMeshAgent.stoppingDistance)
        {
            isIdle = true;
            isRunning = false;
        }
        else
        {
            isIdle = false;
        }
        _playerAnimator.SetBool("IsRunning", isRunning);
        _playerAnimator.SetBool("IsIdle", isIdle);
    }

    private void ChangeMovement(float speed, Vector3 position)
    {
        _playerNavMeshAgent.speed = speed;
        _playerNavMeshAgent.SetPath(_playerNavMeshAgent.path);
        _playerNavMeshAgent.SetDestination(position);
    }

    private void SpawnFlagAtDestination(Vector3 hitPosition)
    {
        if (_flag != null)
        {
            Destroy(_flag);
            Destroy(markedDestinationFlag);
            _hasSpawnedFlag = false;
        }

        if (_hasSpawnedFlag || Time.timeScale == 0) return;

        const float objectHeight = 0.1f;
        
        var spawnPoint = new Vector3(hitPosition.x, hitPosition.y + objectHeight, hitPosition.z);
        
        _flag = Instantiate(clickEffect, spawnPoint, Quaternion.identity);
        markedDestinationFlag = _flag.gameObject;
        _hasSpawnedFlag = true;

    }

    private void DestroyFlagWhenDestinationReached()
    {
        if (!_hasSpawnedFlag || _playerNavMeshAgent.remainingDistance != 0) return;
        
        Destroy(_flag);
        Destroy(markedDestinationFlag);
        _hasSpawnedFlag = false;
    }

    private bool CheckForPath(Vector3 pointToCalculatePath)
    {
        var path = new NavMeshPath();

        _playerNavMeshAgent.CalculatePath(pointToCalculatePath, path);

        return path.status == NavMeshPathStatus.PathComplete;
    }
}

