using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerController : MonoBehaviour
{
    private void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        if (Input.GetKey(KeyCode.F))
        {
            CameraController.instance.followTransform = transform;
        }
    }
}
