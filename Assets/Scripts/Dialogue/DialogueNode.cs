using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu]
    public class DialogueNode : ScriptableObject
    {
        [TextArea]
        public string Dialogue;
        private Texture2D SpeakerSprite;
        public DialogueOption[] Options;
    }

    [Serializable]
    public class DialogueOption
    {
        [TextArea]
        public string Option;
        public DialogueNode Node;
    }
}
