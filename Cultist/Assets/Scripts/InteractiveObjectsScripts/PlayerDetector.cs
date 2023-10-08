using System;
using System.Collections;
using System.Collections.Generic;
using InteractiveObjectsScripts;
using ModestTree;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    private bool _playerDetected;

    [SerializeField] private List<PlayerDetectionAction> _detectionActions;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Start()
    {
        GetComponent<SphereCollider>().radius = detectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            _playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player no longer in range!");
            _playerDetected = false;
        }
    }

    private void DoAction()
    {
        
    }
}
