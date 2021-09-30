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
        public Sprite ButtonSprite;
        public float OptionsHeight = 100;
        
        public float TextSpeed = 100;
        
        public Color DialogueColor;
        public Color ButtonTextColor;
        public Color ButtonColor;

        public UnityEvent<DialogueNode> DialogueFinished;
        
        private RectTransform _textRect;
        private TextMeshProUGUI _text;
        private RectTransform _optionRect;

        private readonly (TextMeshProUGUI text, Button button)[] _buttons = new (TextMeshProUGUI, Button)[3];
    
        // Start is called before the first frame update
        void Start()
        {
            _textRect = new GameObject("Text", typeof(RectTransform)).transform as RectTransform;
            _optionRect = new GameObject("Options", typeof(RectTransform)).transform as RectTransform;
            
            if (_textRect == null) throw new InvalidOperationException("Failed to create RectTransform for text in DialogueRenderer.");
            if (_optionRect == null) throw new InvalidOperationException("Failed to create RectTransform for options in DialogueRenderer.");

            _stretch(_textRect, Vector2.zero, Vector2.one, DialogueArea);
            _stretch(_optionRect, Vector2.zero, Vector2.one, DialogueArea);

            _textRect.offsetMin = Vector2.up * OptionsHeight;
            _optionRect.offsetMax = (DialogueArea.rect.height - OptionsHeight) * Vector2.down;

            _text = _textRect.gameObject.AddComponent<TextMeshProUGUI>();
            _text.color = DialogueColor;

            _buttons[0] = _makeButton("Option 1", new Vector2(0f, 0f), new Vector2(1f, 1 / 3f));
            _buttons[1] = _makeButton("Option 2", new Vector2(0f, 1 / 3f), new Vector2(1f, 2 / 3f));
            _buttons[2] = _makeButton("Option 3", new Vector2(0f, 2 / 3f), new Vector2(1f, 1f));

            StartCoroutine(_run(Root));
        }

        private IEnumerator _run(DialogueNode node)
        {
            _optionRect.gameObject.SetActive(false);

            for (var i = 0; i < 3; i++)
            {
                var (text, button) = _buttons[i];

                if (i >= node.Options.Length)
                {
                    text.gameObject.SetActive(false);
                    button.gameObject.SetActive(false);
                    continue;
                }
                
                text.gameObject.SetActive(true);
                button.gameObject.SetActive(true);
                
                var option = node.Options[i];
                
                text.text = option.Option;
                
                var captured = i;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => StartCoroutine(_run(node.Options[captured].Node)));
            }
            
            var chars = 0f;
            while (chars < node.Dialogue.Length)
            {
                chars += Time.deltaTime * TextSpeed;
                _text.text = node.Dialogue.Substring(0, Math.Min((int)chars, node.Dialogue.Length));
                yield return null;
            }

            _optionRect.gameObject.SetActive(true);
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

            _stretch(t, Vector2.zero, Vector2.one, _optionRect);
            t.anchorMin = anchorMin;
            t.anchorMax = anchorMax;

            image.sprite = ButtonSprite;
            image.color = ButtonColor;
            image.type = Image.Type.Sliced;

            tmp.enableAutoSizing = true;
            tmp.color = ButtonTextColor;
            
            tmp.material.EnableKeyword("UNDERLAY_ON");

            return (tmp, button);
        }
    }
}
