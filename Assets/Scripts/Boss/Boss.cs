using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Boss : Enemy
    {
        protected virtual Phase[] Phases => new Phase[] { };

        private int _startingHealth;
        
        public new void Start()
        {
            base.Start();
            _startingHealth = health;
            StartCoroutine(_lifetime());

            OnDeath += () => SceneManager.LoadScene(SceneManager.Scenes.Map);
        }

        private IEnumerator _lifetime()
        {
            foreach (var phase in Phases)
            {
                while ((float)health / _startingHealth > phase.Health) yield return null;
                phase.PhaseEntered();
            }
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
