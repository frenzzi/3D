using System.Collections;
using UnityEngine;

namespace AdvancedSpawner
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Transform[] _way;
        [SerializeField, Min(0)] private float _speed;

        private bool _isStop = false;

        private void Awake()
        {
            if (_way == null || _way.Length == 0 || _way[0] == null)
            {
                _isStop = true;
                return;
            }

            transform.position = _way[0].position;
        }

        private void Start()
        {
            if (_isStop)
                return;

            StartCoroutine(MoveByTheWay());
        }

        private IEnumerator MoveByTheWay()
        {
            while (isActiveAndEnabled)
            {
                for (int i = 0; i < _way.Length; i++)
                {
                    yield return StartCoroutine(MoveToTarget(_way[i]));
                }

                for (int i = _way.Length - 1; i >= 0; i--)
                {
                    yield return StartCoroutine(MoveToTarget(_way[i]));
                }
            }
        }

        private IEnumerator MoveToTarget(Transform target)
        {
            while (!CheckTarget(target))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

                yield return null;
            }
        }

        private bool CheckTarget(Transform target)
        {
            return target.position.Equals(transform.position);
        }
    }
}
