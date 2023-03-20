using System.Collections.Generic;
using UnityEngine;

public class Container : BaseInteractableObject
{
    private enum ContainerType
    {
        Chest,
        Barrel,
        Basket
    }

    [SerializeField] private ContainerType type;
    public List<BaseItem> containerLoadout;
}
