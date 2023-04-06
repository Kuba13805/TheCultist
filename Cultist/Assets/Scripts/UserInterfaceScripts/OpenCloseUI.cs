using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseUI : MonoBehaviour
{
    public void OpenUI(GameObject uiGameObject)
    {
        uiGameObject.SetActive(true);
    }

    public void CloseUI(GameObject uiGameObject)
    {
        uiGameObject.SetActive(false);
    }
}
