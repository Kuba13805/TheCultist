using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentObject : BaseInteractableObject
{
    [TextAreaAttribute(5, 10)]
    public string info;

    public float displayTime;

    private GameObject panelToDisplay;
    private GameObject panelInstance;
    [SerializeField]private bool hasCommentBeenDisplayed;

    public override void Interact()
    {
        DisplayCommentWindow(info);
        DestroyComment();
    }
    
    private void DisplayCommentWindow(string textToDisplay)
    {
        if (hasCommentBeenDisplayed)
        {
            panelInstance.GetComponent<DisplayCommentOnObject>().DestroyComment(0f);
        }
        var uiPosition = transform.position;
        uiPosition.y += 2;
        panelToDisplay = Resources.Load<GameObject>("CommentsOnObjectsCanvas");
        panelInstance = Instantiate(panelToDisplay, uiPosition, Quaternion.identity);

        panelInstance.GetComponent<DisplayCommentOnObject>().LoadCommentOnObject(textToDisplay);
        hasCommentBeenDisplayed = true;
    }

    private void DestroyComment()
    {
        panelInstance.GetComponent<DisplayCommentOnObject>().DestroyComment(displayTime);
        hasCommentBeenDisplayed = false;
    }
}
