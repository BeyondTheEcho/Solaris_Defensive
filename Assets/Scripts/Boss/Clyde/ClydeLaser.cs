using UnityEngine;

namespace Boss.Clyde
{
    public class ClydeLaser : Laser
    {
        public float Speed;
        
        private float _timeSpawned;
        private Vector3 _positionSpawned;
        private Vector3 _target;
        void Start()
        {
            _timeSpawned = Time.time;
            _positionSpawned = FindObjectOfType<Clyde>().transform.position;
            _target = FindObjectOfType<Player>().transform.position;

            var diff = _positionSpawned - _target;
            transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(diff, Vector2.right));
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.LerpUnclamped(_positionSpawned, _target, (Time.time - _timeSpawned) * Speed);
        }
    }
}
