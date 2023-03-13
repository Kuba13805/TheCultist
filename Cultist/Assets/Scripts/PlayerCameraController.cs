using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCameraController : MonoBehaviour
{
    private Canvas_ShowHidePanel panel;
    public PlayerCameraController instance;
    public Transform cameraTransform;
    public CameraController FreeCameraController;
    public GameObject lockedCamera;
    public GameObject freeCamera;
    private float rotationAmount;
    private bool lockedCameraActive;

    private Vector3 currentPosition;
    
    private Quaternion currentRotation;
    
    void Start()
    {
        panel = FreeCameraController.panel;
        lockedCamera.SetActive(false);
    }
    
    void Update()
    {
        currentPosition = instance.transform.position;
        currentRotation = instance.transform.rotation;
        if (panel.isActive == false)
        {
            SwitchCameras();
        }
    }

    private void SwitchCameras()
    {
        if (Input.GetKeyDown(KeyCode.F) && lockedCameraActive == false)
        {
            lockedCameraActive = true;
            lockedCamera.SetActive(true);
            freeCamera.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && lockedCameraActive)
        {
            lockedCameraActive = false;
            FreeCameraController.newZoom = new Vector3(0, 10, -10);
            FreeCameraController.newPosition = currentPosition;
            FreeCameraController.newRotation = currentRotation;
            freeCamera.SetActive(true);
            lockedCamera.SetActive(false);
        }
    }
}
