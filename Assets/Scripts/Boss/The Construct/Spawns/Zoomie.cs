using System.Collections;
using BehaviourTree;
using UnityEngine;

namespace Boss.The_Construct.Spawns
{
    public class Zoomie : Enemy
    {
        [Header("Zoomie")]
        public float ZoomRadius;
        public float ZoomCooldown;
        public float Speed;
        public float ZoomTime;

        private Transform _player;

        private SelectorNode _root;
        private bool _inZoom = false;
        private bool _canZoom = true;

        // Start is called before the first frame update
        private new void Start()
        {
            base.Start();
            _player = GameObject.FindWithTag("Player").transform;
            
            _root = new SelectorNode
            {
                // If in zoom, carry on
                new ConditionNode(() => _inZoom),
                // Try to walk in radius
                new SequenceNode
                {
                    new ConditionNode(() => (_player.position - transform.position).magnitude > ZoomRadius),
                    new ActionNode(() =>
                    {
                        transform.position = Vector3.MoveTowards(
                            transform.position, 
                            _player.position, 
                            Time.deltaTime * Speed);
                    })
                },
                // If on cooldown, sit still
                new ConditionNode(() => !_canZoom),
                // If not on cooldown, start zooming
                new ActionNode(() => StartCoroutine(_zoom()))
            };
        }

        private new void Update()
        {
            base.Update();
            _root.Evaluate();
        }

        private IEnumerator _zoom()
        {
            _inZoom = true;
            var start = transform.position;
            var end = start + 2 * (_player.position - transform.position);

            for (var i = 0f; i < ZoomTime; i += Time.deltaTime)
            {
                transform.position = Vector3.LerpUnclamped(start, end, i / ZoomTime);
                yield return null;
            }

            _inZoom = false;
            _canZoom = false;
            yield return new WaitForSeconds(ZoomCooldown);
            _canZoom = true;
        }
    }
}
