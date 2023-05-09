using DG.Tweening.Plugins.Options;
using Managers;
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

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private Vector3 _newPosition;

    private Vector3 _newZoom;

    private Vector3 _rotateStartPosition;

    private Vector3 _rotateCurrentPosition;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

        var transform1 = transform;
        _newPosition = transform1.position;
        _newZoom = cameraTransform.localPosition;

        _cameraActions = InputManager.Instance.PlayerInputActions;
        _movement = _cameraActions.Camera.MoveCamera;

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
        var inputValue = _cameraActions.Camera.RotateCamera.ReadValue<Vector2>().x;

        if (_cameraActions.Camera.RotateCameraOnClick.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0f, inputValue * rotationAmount + transform.rotation.eulerAngles.y, 0f);
        }
    }

    private void HandleMouseZoom()
    {
        var inputValue = _cameraActions.Camera.ZoomCamera.ReadValue<Vector2>().y;
        inputValue *= 0.25f;
        
        if (inputValue > 0 || cameraTransform.localPosition.y <= minZoom)
        {
            _newZoom -= zoomAmount;

        }
        if (inputValue < 0 || cameraTransform.localPosition.y >= maxZoom)
        {
            _newZoom += zoomAmount;
        }
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _newZoom, Time.deltaTime * movementTime);
    }
    private void HandleKeyboardInput()
    {
        HandleKeyboardMovement();
    }
    private void HandleKeyboardMovement()
    {
        var inputValue = _movement.ReadValue<Vector2>().x * GetCameraRight() +
                         _movement.ReadValue<Vector2>().y * GetCameraForward();
        
        inputValue = inputValue.normalized;
        inputValue *= movementSpeed;

        if (inputValue.sqrMagnitude > 0.03f)
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
