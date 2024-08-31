using UnityEngine;

namespace AdvancedSpawner
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _speed = 0.5f;

        private Target _target;

        private void Update()
        {
            MoveToTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _target.gameObject)
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(Target target)
        {
            _target = target;
        }

        private void MoveToTarget()
        {
            transform.up = _target.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
    }
}

