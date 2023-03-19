using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string objectName;
    public int objectId;
    private List<string> contextMenuOptions = new List<string>();
    private bool isContextMenuOpen = false;

    void Start()
    {
        // Add context menu options
        contextMenuOptions.Add("Option 1");
        contextMenuOptions.Add("Option 2");
        contextMenuOptions.Add("Option 3");
    }

    void Update()
    {
        if (isContextMenuOpen && Input.GetKeyDown(KeyCode.T))
        {
            CloseContextMenu();
        }
    }

    void OnGUI()
    {
        if (isContextMenuOpen)
        {
            // Set up the context menu
            float menuWidth = 150f;
            float menuHeight = contextMenuOptions.Count * 30f;
            Rect menuRect = new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, menuWidth, menuHeight);

            // Draw the context menu
            GUI.Box(menuRect, "");
            for (int i = 0; i < contextMenuOptions.Count; i++)
            {
                if (GUI.Button(new Rect(menuRect.x, menuRect.y + i * 30f, menuWidth, 30f), contextMenuOptions[i]))
                {
                    // Handle the context menu selection
                    HandleContextMenuSelection(contextMenuOptions[i]);
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Open the context menu
            OpenContextMenu();
        }
    }

    void OpenContextMenu()
    {
        isContextMenuOpen = true;
    }

    void CloseContextMenu()
    {
        isContextMenuOpen = false;
    }

    void HandleContextMenuSelection(string selection)
    {
        Debug.Log("Selected option: " + selection);
        // Add your own custom code to handle the context menu selection here
    }
}
