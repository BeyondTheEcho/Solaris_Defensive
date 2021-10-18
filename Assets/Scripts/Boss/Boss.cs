using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Boss : Enemy
    {
        protected virtual Phase[] Phases => new Phase[] { };

        public event Action OnDeath = () => { };

        private int _startingHealth;
        
        public new void Start()
        {
            base.Start();
            _startingHealth = health;
            StartCoroutine(_lifetime());
        }

        private IEnumerator _lifetime()
        {
            foreach (var phase in Phases)
            {
                while ((float)health / _startingHealth > phase.Health) yield return null;
                phase.PhaseEntered();
            }
        }
        
        protected void OnDestroy()
        {
            OnDeath();
            SceneManager.LoadScene(SceneManager.Scenes.Map);
        }
    }

    public class Phase
    {
        public float Health;
        public Action PhaseEntered;

        public Phase(float health, Action phaseEntered)
        {
            Health = health;
            PhaseEntered = phaseEntered;
        }
    }
}
