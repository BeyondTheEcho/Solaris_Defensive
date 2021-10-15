using System.Collections;
using UnityEngine;

namespace Boss.Clyde
{
    public class Clyde : Boss
    {
        [Header("Clyde")]
        public Transform Top;
        public Transform Bottom;
        public float FireRate;

        public GameObject LaserPrefab;
        public SpriteRenderer Renderer;

        private float _percentToShot = 0f;

        protected override Phase[] Phases => new[]
        {
            new Phase(0.5f, () => StartCoroutine(_enrage()))
        };
        
        private new void Update()
        {
            base.Update();
            
            transform.localPosition = Vector3.Lerp(Bottom.localPosition, Top.localPosition, _sin(Time.time, 0f, 1f));
            
            _percentToShot += Time.deltaTime * FireRate;
            if (_trySubtract(ref _percentToShot, 1f))
            {
                Instantiate(LaserPrefab);
            }
        }

        private IEnumerator _enrage()
        {
            FireRate *= 2;
            for (var i = 0f; i <= 1f; i += Time.deltaTime)
            {
                Renderer.color = Color.Lerp(Color.white, Color.red, i);
                yield return null;
            }
        }

        private float _sin(float x, float min, float max) => Mathf.Sin(x) * (max - min) / 2 + (max + min) / 2;
    }
}