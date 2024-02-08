using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class NarrativeEventId
{
    [AllowNesting]
    public Campaign eventCampaign;
    [AllowNesting]
    public int eventNumber;
}
