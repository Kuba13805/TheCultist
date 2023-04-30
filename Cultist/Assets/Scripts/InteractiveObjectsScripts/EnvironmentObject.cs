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

    private GameObject _panelToDisplay;
    private GameObject _panelInstance;
    private bool _hasCommentBeenDisplayed;

    public override void Interact()
    {
        DisplayCommentWindow(info);
        DestroyComment();
    }
    
    private void DisplayCommentWindow(string textToDisplay)
    {
        if (_hasCommentBeenDisplayed)
        {
            _panelInstance.GetComponent<DisplayCommentOnObject>().DestroyComment(0f);
        }
        var uiPosition = transform.position;
        uiPosition.y += 2;
        _panelToDisplay = Resources.Load<GameObject>("CommentsOnObjectsCanvas");
        _panelInstance = Instantiate(_panelToDisplay, uiPosition, Quaternion.identity);

        _panelInstance.GetComponent<DisplayCommentOnObject>().LoadCommentOnObject(textToDisplay);
        _hasCommentBeenDisplayed = true;
    }

    private void DestroyComment()
    {
        _panelInstance.GetComponent<DisplayCommentOnObject>().DestroyComment(displayTime);
        _hasCommentBeenDisplayed = false;
    }
}
