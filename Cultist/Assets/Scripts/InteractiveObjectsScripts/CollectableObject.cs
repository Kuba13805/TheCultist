using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    [TextArea(15, 20)]
    public string text;
    
    [SerializeField] private CollectableObjectType type;
    
    private GameObject _panelToDisplay;
    
    private GameObject _panelInstance;

    [FormerlySerializedAs("IsActive")] public bool isActive;
    public override void Interact()
    {
        isActive = false;
        
        ShowPanel();
    }

    private void ShowPanel()
    {
        DeterminePanelToShow();
        if (Time.timeScale == 0) return;
        
        if (isActive)
        {
            Destroy(_panelInstance.gameObject);
        }
        
        Canvas canvas = FindObjectOfType<Canvas>();
        _panelInstance = Instantiate(_panelToDisplay, canvas.transform, false);
        
        LoadText("CollectableDesc");
        LoadText("CollectableTitle");
        
        isActive = true;
    }

    private void LoadText(string elementToFind)
    {
        switch (type)
        {
            case CollectableObjectType.Book:
            {
                GameObject textToDisplay = _panelInstance.transform.Find(elementToFind).gameObject;
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
        _panelToDisplay = type switch
        {
            CollectableObjectType.Newspaper => Resources.Load<GameObject>("CollectableNewspaperPanel"),
            CollectableObjectType.Book => Resources.Load<GameObject>("CollectableBookPanel"),
            CollectableObjectType.Scroll => Resources.Load<GameObject>("CollectableScrollPanel"),
            CollectableObjectType.Note => Resources.Load<GameObject>("CollectableNotePanel"),
            _ => _panelToDisplay
        };
    }
}
