using DG.Tweening.Plugins.Options;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private PlayerInputActions _cameraActions;

    private GameObject _player;
    
    [SerializeField] private float movementSpeed;

    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private float mouseRotationSpeed;
    
    private float _minZoom, _maxZoom;

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
        _cameraActions.Camera.Enable();

        _cameraActions.Camera.CameraFocusOnPlayer.performed += FocusCamera;
        TravelPoint.OnPlayerTravelDone += MoveCameraToTravelPoint;
    }

    private void Update()
    {
        UpdateZoomLimits();
        HandleMouseInput();
        HandleKeyboardInput();
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
        if (Input.mouseScrollDelta.y > 0 || cameraTransform.localPosition.y <= _minZoom)
        {
            _newZoom -= zoomAmount;
            
        }
        if (Input.mouseScrollDelta.y < 0 || cameraTransform.localPosition.y >= _maxZoom)
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
        if (Input.GetKey(KeyCode.W))
        {
            _newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -movementSpeed);
        }
        
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * movementTime);
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

    private void UpdateZoomLimits()
    {
        var position = _player.transform.position;
        
        _minZoom = position.y - 10f;
        _maxZoom = position.y + 10f;
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
        _newZoom.y = _maxZoom + Mathf.Abs(_minZoom) / 2;
        _newZoom.z = -(_maxZoom + Mathf.Abs(_minZoom) / 2);
    }
}
