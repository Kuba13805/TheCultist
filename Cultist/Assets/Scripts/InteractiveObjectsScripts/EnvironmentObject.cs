using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentObject : BaseInteractableObject
{
    [TextAreaAttribute(10, 15)]
    public string text;

    public override void Interact()
    {
        
    }
}
