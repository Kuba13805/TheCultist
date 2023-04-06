using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public void CloseMenu()
    {
        GameManager.Instance.ResumeGame();
        Destroy(gameObject);
    }
}
