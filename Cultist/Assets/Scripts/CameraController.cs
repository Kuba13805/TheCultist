using DG.Tweening.Plugins.Options;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private PlayerInputActions _cameraActions;
    private InputAction _movement;

    private GameObject _player;
    
    [SerializeField] private float movementSpeed;

    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private float mouseRotationSpeed;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private Vector3 _newPosition;

    private Quaternion _newRotation;

    private Vector3 _newZoom;

    private Vector3 _rotateStartPosition;

    private Vector3 _rotateCurrentPosition;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = cameraTransform.localPosition;

        _cameraActions = new PlayerInputActions();
        _movement = _cameraActions.Camera.MoveCamera;
        _cameraActions.Camera.Enable();

        _cameraActions.Camera.CameraFocusOnPlayer.performed += FocusCamera;
        TravelPoint.OnPlayerTravelDone += MoveCameraToTravelPoint;
    }

    private void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
        FollowPlayerPositionY();
    }

    private void HandleMouseInput()
    {
        HandleMouseRotation();
        HandleMouseZoom();
    }

    private void HandleMouseRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            _rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = _rotateStartPosition - _rotateCurrentPosition;

            _rotateStartPosition = _rotateCurrentPosition;
            
            _newRotation *= quaternion.Euler(Vector3.up * (-difference.x / mouseRotationSpeed));
        }
    }

    private void HandleMouseZoom()
    {
        if (Input.mouseScrollDelta.y > 0 || cameraTransform.localPosition.y <= minZoom)
        {
            _newZoom -= zoomAmount;
            
        }
        if (Input.mouseScrollDelta.y < 0 || cameraTransform.localPosition.y >= maxZoom)
        {
            _newZoom += zoomAmount;
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _newZoom, Time.deltaTime * movementTime);
    }
    private void HandleKeyboardInput()
    {
        HandleKeyboardMovement();
        HandleKeyboardRotation();
    }
    private void HandleKeyboardMovement()
    {
        var inputValue = _movement.ReadValue<Vector2>().x * GetCameraRight() +
                         _movement.ReadValue<Vector2>().y * GetCameraForward();
        
        inputValue = inputValue.normalized;
        inputValue *= 0.25f;

        if (inputValue.sqrMagnitude > 0.05f)
        {
            _newPosition += inputValue;
        }
        
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * movementTime);
    }
    private Vector3 GetCameraRight()
    {
        var right = cameraTransform.right;
        right.y = 0;
        return right;
    }
    
    private Vector3 GetCameraForward()
    {
        var forward = cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private void HandleKeyboardRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _newRotation *= Quaternion.Euler((Vector3.up) * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            _newRotation *= Quaternion.Euler((Vector3.up) * -rotationAmount);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * movementTime);
    }

    private void FocusCamera(InputAction.CallbackContext obj)
    {
        MoveCameraToPoint();
    }

    private void MoveCameraToTravelPoint()
    {
        MoveCameraToPoint();
    }
    private void MoveCameraToPoint()
    {
        var transformPosition = transform.position;
        
        transformPosition = _player.transform.position;

        _newPosition = transformPosition;
    }

    private void FollowPlayerPositionY()
    {
        // var transformPosition = transform.position;
        //
        // transformPosition.y = _player.transform.position.y;
        // transformPosition.y -= 0.03f;
        //
        // _newPosition.y = transformPosition.y;
    }
}
