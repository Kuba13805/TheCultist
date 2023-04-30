using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInfoScript : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = GameObject.Find("MainCamera").transform.rotation;
    }

    private void Update()
    {
        transform.rotation = GameObject.Find("MainCamera").transform.rotation;
    }
}
