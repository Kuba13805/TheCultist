using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorScript : MonoBehaviour
{
    [HideInInspector] public Vector3 interactorPosition;
    [HideInInspector] public Quaternion interactorRotation;

    private void Start()
    {
        interactorRotation = transform.rotation;
        interactorPosition = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
