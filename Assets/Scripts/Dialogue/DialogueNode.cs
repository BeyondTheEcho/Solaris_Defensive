using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu]
    public class DialogueNode : ScriptableObject
    {
        [TextArea]
        public string Dialogue;
        public Sprite SpeakerSprite;
        public SpeakerPosition SpeakerPosition;
        public DialogueOption[] Options;
    }

    [Serializable]
    public class DialogueOption
    {
        [TextArea]
        public string Option;
        public DialogueNode Node;
    }

    public enum SpeakerPosition
    {
        Left,
        Right
    }
}
