using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    private GameObject _player;
    private PlayerInputActions _cameraActions;
    private InputAction _movement;
    private Transform _cameraTransform;

    [Header("Horizontal Translation")]
    [SerializeField] private float maxSpeed = 5f;
    private float _speed;
    [Header("Horizontal Translation")]
    [SerializeField] private float acceleration = 10f;
    [Header("Horizontal Translation")]
    [SerializeField] private float damping = 15f;

    [Header("Vertical Translation")]
    [SerializeField] private float stepSize = 2f;
    [Header("Vertical Translation")]
    [SerializeField] private float zoomDampening = 7.5f;
    private float _minZoom;
    private float _maxZoom;
    [Header("Vertical Translation")]
    [SerializeField] private float zoomSpeed = 2f;

    [Header("Rotation")]
    [SerializeField] private float maxRotationSpeed = 1f;

    [Header("Edge Movement")]
    [SerializeField] [Range(0f,0.1f)] private float edgeTolerance = 0.05f;

    private Vector3 _targetPosition;

    private float _zoomHeight;
    
    private Vector3 _horizontalVelocity;
    private Vector3 _lastPosition;
    
    private Vector3 _startDrag;

    private void Awake()
    {
        _cameraActions = new PlayerInputActions();
        _cameraTransform = transform;
    }

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        _lastPosition = transform.position;

        _cameraActions.Camera.RotateCamera.performed += RotateCamera;
        _cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
    }

    private void OnDisable()
    {
        _cameraActions.Camera.RotateCamera.performed -= RotateCamera;
        _cameraActions.Camera.ZoomCamera.performed -= ZoomCamera;

        _cameraActions.Camera.Disable();
    }

    private void Update()
    {
        GetKeyboardMovement();
        
        UpdateZoomLimits();
        UpdateVelocity();
        UpdateBasePosition();
        UpdateCameraPosition();
    }

    private void UpdateVelocity()
    {
        var position = transform.position;
        
        _horizontalVelocity = (position - _lastPosition) / Time.deltaTime;
        _horizontalVelocity.y = 0;
        _lastPosition = position;
    }

    private void GetKeyboardMovement()
    {
        var inputValue = _movement.ReadValue<Vector2>().x * GetCameraRight() +
                         _movement.ReadValue<Vector2>().y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
        {
            _targetPosition += inputValue;
        }
    }
    
    private Vector3 GetCameraRight()
    {
        var right = _cameraTransform.right;
        right.y = 0;
        return right;
    }
    
    private Vector3 GetCameraForward()
    {
        var forward = _cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private void UpdateBasePosition()
    {
        if (_targetPosition.sqrMagnitude > 0.1f)
        {
            _speed = Mathf.Lerp(_speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += _targetPosition * _speed * Time.deltaTime;
        }
        else
        {
            _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += _horizontalVelocity * Time.deltaTime;
        }

        _targetPosition = Vector3.zero;
    }
    
    private void RotateCamera(InputAction.CallbackContext inputValue)
    {
        if (Mouse.current.rightButton.isPressed) return;

        var value = inputValue.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, value * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }
    
    private void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        var value = inputValue.ReadValue<Vector2>().y / 100f;

        if (!(Mathf.Abs(value) > 0.1f)) return;
        _zoomHeight = _cameraTransform.localPosition.y + value * stepSize;
        if (_zoomHeight < _minZoom)
        {
            _zoomHeight = _minZoom;
        }
        else if (_zoomHeight > _maxZoom)
        {
            _zoomHeight = _maxZoom;
        }
    }

    private void UpdateCameraPosition()
    {
        var localPosition = _cameraTransform.localPosition;
        
        var zoomTarget =
            new Vector3(localPosition.x, _zoomHeight, localPosition.z);
        zoomTarget -= zoomSpeed * (_zoomHeight - localPosition.y) * Vector3.forward;

        localPosition = Vector3.Lerp(localPosition, zoomTarget, Time.deltaTime * zoomDampening);
    }
    
    private void UpdateZoomLimits()
    {
        var position = _player.transform.position;
        
        _minZoom = position.y - 10f;
        _maxZoom = position.y + 10f;
    }
}
