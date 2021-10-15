using UnityEngine;

namespace Boss.The_Construct.Spawns
{
    public class Nemesis : MonoBehaviour
    {
        public float Speed;
    
        private Transform _player;
        void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }
    
        void Update()
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                _player.position + Vector3.right * 5,
                Time.deltaTime * Speed);
        }
    }
}
