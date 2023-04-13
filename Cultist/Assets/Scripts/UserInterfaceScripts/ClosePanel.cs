using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public void CloseMenu()
    {
        if (Time.timeScale == 0)
        {
            GameManager.Instance.ResumeGame();
        }
        Destroy(gameObject);
    }
}
