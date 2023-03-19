using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : BaseInteractableObject
{
    private enum containerType
    {
        Chest,
        Barrel,
        Basket
    }

    [SerializeField] private containerType type;
    public List<BaseItem> containerLoadout;
}
