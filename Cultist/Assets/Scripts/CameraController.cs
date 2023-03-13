using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Canvas_ShowHidePanel panel;
    public static CameraController instance;
    
    public Transform followTransform;
    public Transform cameraTransform;
    public float movementSpeed;

    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public float mouseRotationSpeed;
    public float minZoom, maxZoom;

    public Vector3 newPosition;

    public Quaternion newRotation;

    public Vector3 newZoom;

    public Vector3 rotateStartPosition;

    public Vector3 rotateCurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.isActive == false)
        {
            //FollowPlayer();
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
    /*
    private void FollowPlayer()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            followTransform = null;
        }
    }
    */
}
