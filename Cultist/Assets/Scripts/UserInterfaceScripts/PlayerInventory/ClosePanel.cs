using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public void CloseMenu()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
