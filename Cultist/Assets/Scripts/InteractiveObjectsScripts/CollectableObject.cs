using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
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
        Note
    };

    public string title;
    
    [TextAreaAttribute(15, 20)]
    public string text;
    
    [SerializeField] private CollectableObjectType type;
    
    private GameObject panelToDisplay;
    
    private GameObject panelInstance;

    public static event Action OnCollectableShown;
    
    public static event Action OnCollectableClosed;

    public bool IsActive;
    public override void Interact()
    {
        IsActive = false;
        
        ShowPanel();
        
        GameManager.Instance.PauseGame();
    }

    private void ShowPanel()
    {
        DeterminePanelToShow();
        if (Time.timeScale == 0) return;
        
        if (IsActive)
        {
            Destroy(panelInstance.gameObject);
        }
        
        Canvas canvas = FindObjectOfType<Canvas>();
        panelInstance = Instantiate(panelToDisplay, canvas.transform, false);
        
        LoadText("CollectableDesc");
        LoadText("CollectableTitle");
        
        IsActive = true;
    }

    private void LoadText(string elementToFind)
    {
        switch (type)
        {
            case CollectableObjectType.Book:
            {
                GameObject textToDisplay = panelInstance.transform.Find(elementToFind).gameObject;
                textToDisplay.GetComponent<TextMeshProUGUI>().text = elementToFind switch
                {
                    "CollectableDesc" => text,
                    "CollectableTitle" => title,
                    _ => textToDisplay.GetComponent<TextMeshProUGUI>().text
                };

                break;
            }
            default:
            {
                GameObject textToDisplay = GameObject.Find(elementToFind);

                textToDisplay.GetComponent<TextMeshProUGUI>().text = elementToFind switch
                {
                    "CollectableDesc" => text,
                    "CollectableTitle" => title,
                    _ => textToDisplay.GetComponent<TextMeshProUGUI>().text
                };

                break;
            }
        }
    }

    private void DeterminePanelToShow()
    {
        panelToDisplay = type switch
        {
            CollectableObjectType.Newspaper => Resources.Load<GameObject>("CollectableNewspaperPanel"),
            CollectableObjectType.Book => Resources.Load<GameObject>("CollectableBookPanel"),
            CollectableObjectType.Scroll => Resources.Load<GameObject>("CollectableScrollPanel"),
            CollectableObjectType.Note => Resources.Load<GameObject>("CollectableNotePanel"),
            _ => panelToDisplay
        };
    }
}
