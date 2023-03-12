using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GameObject markedDestination = null;
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
        if (Input.GetMouseButton(0))
        {
            Ray myRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                PlayerNavMeshAgent.SetDestination(myRaycastHit.point);
                
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

