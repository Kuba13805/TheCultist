using DG.Tweening.Plugins.Options;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public Canvas_ShowHidePanel panel;

    private GameObject player;
    
    public float movementSpeed;

    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public float mouseRotationSpeed;
    
    private float minZoom, maxZoom;

    public Vector3 newPosition;

    public Quaternion newRotation;

    public Vector3 newZoom;

    public Vector3 rotateStartPosition;

    public Vector3 rotateCurrentPosition;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    
    void Update()
    {
        UpdateZoomLimits();
        if (panel.isActive == false)
        {
            HandleMouseInput();
            HandleKeyboardInput();
        }
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
            rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;
            
            newRotation *= quaternion.Euler(Vector3.up * (-difference.x / mouseRotationSpeed));
        }
    }

    private void HandleMouseZoom()
    {
        if (Input.mouseScrollDelta.y > 0 || cameraTransform.localPosition.y <= minZoom)
        {
            newZoom -= zoomAmount;
            
        }
        if (Input.mouseScrollDelta.y < 0 || cameraTransform.localPosition.y >= maxZoom)
        {
            newZoom += zoomAmount;
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
    private void HandleKeyboardInput()
    {
        HandleKeyboardMovement();
        HandleKeyboardRotation();
    }
    private void HandleKeyboardMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    private void HandleKeyboardRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler((Vector3.up) * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler((Vector3.up) * -rotationAmount);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }

    private void UpdateZoomLimits()
    {
        minZoom = player.transform.position.y - 10f;
        maxZoom = player.transform.position.y + 10f;
    }
}
