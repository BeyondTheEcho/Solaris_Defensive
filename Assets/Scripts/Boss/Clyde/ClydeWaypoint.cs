using System;
using UnityEngine;

namespace Boss.Clyde
{
    public class ClydeWaypoint : MonoBehaviour
    {
        public float Speed;
        public Vector3 Begin;
        public Vector3 End;
        
        void Start()
        {
            transform.localPosition = Begin;
        }

        void Update()
        {
            var a = (Time.time * Speed) % 2;
            if (a > 1f) a = 2 - a;
            transform.localPosition = Vector3.Lerp(Begin, End, a);
        }

        private void OnValidate()
        {
            transform.position = Begin;
        }
    }
}
