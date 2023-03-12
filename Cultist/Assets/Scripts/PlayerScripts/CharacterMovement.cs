using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GameObject markedDestinationFlag = null;
    public Canvas_ShowHidePanel panel;
    public Animator playerAnimator;
    public bool isIdle;
    public NavMeshAgent PlayerNavMeshAgent;


    public Camera PlayerCamera;

    void Update()
    {
        IsMoving();
        MovePlayerToPosition();
    }

    private void MovePlayerToPosition()
    {
        if (panel.isActive == false)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit myRaycastHit;
                Ray myRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(myRay, out myRaycastHit))
                {
                    PlayerNavMeshAgent.SetDestination(myRaycastHit.point);
                }
            }
        }
    }

    private void IsMoving()
    {
        IsIdle();
    }

    private void IsIdle()
    {
        if (PlayerNavMeshAgent.remainingDistance <= PlayerNavMeshAgent.stoppingDistance)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
        
        playerAnimator.SetBool("IsIdle", isIdle);
    }
}

