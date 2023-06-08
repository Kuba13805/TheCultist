using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UserInterfaceScripts.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        public TextAsset dialogueInstance;

        public bool oneTimeDialogue;
        
        public bool wasSeen;

        public List<DialogueRequiredQuest> RequiredQuests;
    }
}