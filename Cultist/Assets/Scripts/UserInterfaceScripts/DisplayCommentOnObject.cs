using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCommentOnObject : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = GameObject.Find("MainCamera").transform.rotation;
    }

    private void Update()
    {
        transform.rotation = GameObject.Find("MainCamera").transform.rotation;
    }

    public void LoadCommentOnObject(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void DestroyComment(float time)
    {
        Destroy(gameObject, time);
    }
}
