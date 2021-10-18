using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu]
    public class DialogueNode : ScriptableObject
    {
        [TextArea]
        public string Dialogue;
        public string SpeakerName;
        public Sprite SpeakerSpriteLeft;
        public Sprite SpeakerSpriteRight;
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
