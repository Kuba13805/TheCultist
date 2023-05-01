using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorScript : MonoBehaviour
{
    public Vector3 interactorPosition;
    public Quaternion interactorRotation;

    private void Start()
    {
        interactorRotation = transform.rotation;
        interactorPosition = transform.position;
    }
}
