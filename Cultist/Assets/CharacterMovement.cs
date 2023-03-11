using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public NavMeshAgent PlayerNavMeshAgent;

    public Camera PlayerCamera;

    void Update()
    {
        MovePlayerToPosition();
    }

    void MovePlayerToPosition()
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
}
