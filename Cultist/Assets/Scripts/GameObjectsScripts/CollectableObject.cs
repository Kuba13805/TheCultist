using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectableObject : BaseInteractableObject
{
    private enum CollectableObjectType
    {
        Newspaper,
        Book,
        Scroll,
        Leaflet
    };
    
    public string text;
    [SerializeField] private CollectableObjectType type;
    private GameObject panelToDisplay;
    private GameObject panelInstance;

    public bool IsActive;
    public override void Interact()
    {
        IsActive = false;
        ShowPanel();
        PauseGame();
    }

    private void ShowPanel()
    {
        DeterminePanelToShow();
        if (Time.timeScale != 0)
        {
            if (IsActive)
            {
                Destroy(panelInstance.gameObject);
            }
            Canvas canvas = FindObjectOfType<Canvas>();
            panelInstance = Instantiate(panelToDisplay, canvas.transform, false);
            Instantiate(panelInstance);
            LoadText("CollectableDesc");
            LoadText("CollectableTitle");
            IsActive = true;
        }
    }

    private void LoadText(string elementToFind)
    {
        GameObject textToDisplay = panelInstance.transform.Find(elementToFind).gameObject;
        textToDisplay.GetComponent<TextMeshProUGUI>().text = text;
    }

    private void DeterminePanelToShow()
    {
        if (type == CollectableObjectType.Newspaper)
        {
            panelToDisplay = Resources.Load<GameObject>("CollectableNewspaperPanel");
        }
        if (type == CollectableObjectType.Book)
        {
            panelToDisplay = Resources.Load<GameObject>("CollectableBookPanel");
        }
        if (type == CollectableObjectType.Scroll)
        {
            panelToDisplay = Resources.Load<GameObject>("CollectableScrollPanel");
        }
        if (type == CollectableObjectType.Leaflet)
        {
            panelToDisplay = Resources.Load<GameObject>("CollectableLeafletPanel");
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
