using System.Collections;
using System.Collections.Generic;
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
    
    public Text text;
    [SerializeField] private CollectableObjectType type;
}
