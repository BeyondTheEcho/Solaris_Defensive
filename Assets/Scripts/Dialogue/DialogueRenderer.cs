using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueRenderer : MonoBehaviour
    {
        public DialogueNode Root;
        public RectTransform DialogueArea;
        public Image LeftSpeaker;
        public Image RightSpeaker;
        
        public Sprite ButtonSprite;
        public float OptionsHeight = 100;
        public float SpeakerHeight = 50;
        
        public float TextSpeed = 100;
        public float FontSize = 30;
        
        public Color DialogueColor;
        public Color ButtonTextColor;
        public Color ButtonColor;

        public bool StartAutomatically;

        public UnityEvent<DialogueNode> DialogueFinished;
        
        
        public RectTransform SpeakerRect;
        private TextMeshProUGUI _speakerText;
        public RectTransform TextRect;
        private TextMeshProUGUI _text;
        public RectTransform OptionRect;

        private (TextMeshProUGUI text, Button button)[] _buttons = {};
    
        // Start is called before the first frame update
        void Awake()
        {

            _speakerText = SpeakerRect.gameObject.AddComponent<TextMeshProUGUI>();
            _text = TextRect.gameObject.AddComponent<TextMeshProUGUI>();
            _text.fontSize = _speakerText.fontSize = FontSize;
            _text.color = _speakerText.color = DialogueColor;

            if(StartAutomatically) StartCoroutine(_run(Root));
        }

        public void Run()
        {
            StartCoroutine(_run(Root));
        }

        private IEnumerator _run(DialogueNode node)
        {
            OptionRect.gameObject.SetActive(false);

            _speakerText.text = node.SpeakerName;

            if (LeftSpeaker) LeftSpeaker.color = Color.clear;
            if (RightSpeaker) RightSpeaker.color = Color.clear;

            if (node)
            {
                if (node.SpeakerSpriteLeft)
                {
                    LeftSpeaker.sprite = node.SpeakerSpriteLeft;
                    LeftSpeaker.color = Color.white;
                }

                if (node.SpeakerSpriteRight)
                {
                    RightSpeaker.sprite = node.SpeakerSpriteRight;
                    RightSpeaker.color = Color.white;
                }
            }

            foreach (var (t, b) in _buttons)
            {
                Destroy(b.gameObject);
            }
            
            _buttons = new (TextMeshProUGUI text, Button button)[node.Options.Length];
            
            for (var i = 0; i < node.Options.Length; i++)
            {
                var option = node.Options[i];
                
                var (t, b) = _buttons[i] = _makeButton(
                    $"Option {i}",
                    new Vector2(0f, (float)i / node.Options.Length),
                    new Vector2(1f, (float)(i + 1) / node.Options.Length));

                t.text = option.Option;
                
                var captured = i;
                var next = node.Options[captured].Node;
                if (next) b.onClick.AddListener(() => StartCoroutine(_run(next)));
                else b.onClick.AddListener(() => DialogueFinished.Invoke(node));
            }
            
            var chars = 0f;
            while (chars < node.Dialogue.Length)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    chars = node.Dialogue.Length;
                }
                chars += Time.deltaTime * TextSpeed;
                _text.text = node.Dialogue.Substring(0, Math.Min((int)chars, node.Dialogue.Length));
                yield return null;
            }

            OptionRect.gameObject.SetActive(true);
            
            if (node.Options.Length == 0)
            {
                yield return new WaitForSeconds(1f);
                DialogueFinished.Invoke(node);
            }
        }
        
        private static void _stretch(RectTransform t, Vector2 offsetMin, Vector2 offsetMax, RectTransform parent = null)
        {
            if (parent != null) t.SetParent(parent, false);
            t.pivot = new Vector2(0.5f, 0.5f);
            t.anchorMax = Vector2.one;
            t.anchorMin = Vector2.zero;
            t.sizeDelta = Vector2.zero;
            t.offsetMin = offsetMin;
            t.offsetMax = offsetMax;
        }

        private (TextMeshProUGUI, Button) _makeButton(string name, Vector2 anchorMin, Vector2 anchorMax)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Button),  typeof(Image));
            var t = go.transform as RectTransform;
            var button = go.GetComponent<Button>();
            var image = go.GetComponent<Image>();
            
            var text = new GameObject("Text", typeof(TextMeshProUGUI));
            var textTransform = text.transform as RectTransform;
            _stretch(textTransform, Vector2.right * 10, Vector2.one, t);
            var tmp = text.GetComponent<TextMeshProUGUI>();

            _stretch(t, Vector2.zero, Vector2.one, OptionRect);
            t.anchorMin = anchorMin;
            t.anchorMax = anchorMax;

            image.sprite = ButtonSprite;
            image.color = ButtonColor;
            image.type = Image.Type.Sliced;

            tmp.enableAutoSizing = false;
            tmp.fontSize = 30f;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.color = ButtonTextColor;
            tmp.material.EnableKeyword("UNDERLAY_ON");
            
            return (tmp, button);
        }
    }
}
