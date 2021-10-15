using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boss.The_Construct
{
    public class TheConstruct : Boss
    {
        [Header("The Construct")]
        public Transform[] Spawners;
        public GameObject[] Enemies;
        public float SpawnRate;

        private float _percentToNextSpawn = 0.8f;
        
        protected override Phase[] Phases => new[]
        {
            new Phase(0.9f, () => _spawn(1)),
            new Phase(0.8f, () => _spawn(1)),
            new Phase(0.7f, () => _spawn(3)),
            new Phase(0.6f, () => _spawn(2)),
            new Phase(0.5f, () => _spawn(5)),
            new Phase(0.45f, () => _spawn(1)),
            new Phase(0.40f, () => _spawn(3)),
            new Phase(0.35f, () => _spawn(1)),
            new Phase(0.30f, () => _spawn(4)),
            new Phase(0.25f, () => _spawn(1)),
            new Phase(0.20f, () => _spawn(5)),
            new Phase(0.15f, () => _spawn(6)),
            new Phase(0.10f, () => _spawn(2)),
            new Phase(0.05f, () => _spawn(3))
        };

        new void Update()
        {
            base.Update();
            _percentToNextSpawn += Time.deltaTime * SpawnRate;
            if (_trySubtract(ref _percentToNextSpawn, 1f))
            {
                var n = 1;
                var rand = Random.Range(0, 100);
                if (rand > 50) n++;
                if (rand > 75) n++;
                if (rand > 90) n++;
                _spawn(n);
            }
        }
        
        private void _spawn(int n)
        {
            for (var i = 0; i < n; i++)
            {
                var go = Instantiate(_getRandomEnemy(), transform, true);
                go.transform.position = _getRandomSpawner().position;
            }
        }

        private Transform _getRandomSpawner() => Spawners[Random.Range(0, Spawners.Length)];
        private GameObject _getRandomEnemy() => Enemies[Random.Range(0, Enemies.Length)];
    }
}
