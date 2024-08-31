using UnityEngine;

namespace CodeStyleGenuis
{
    public class MoverByPoints : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _placesParent;

        private Transform[] _places;
        private int _numberOfCurrentPlace = 0;

        private void Awake()
        {
            _places = new Transform[_placesParent.childCount];

            for (int i = 0; i < _placesParent.childCount; i++)
            {
                _places[i] = _placesParent.GetChild(i);
            }

            if (_places.Length > 0)
            {
                Vector3 direction = _places[_numberOfCurrentPlace].position - transform.position;
                transform.forward = direction.normalized;
            }
        }

        private void Update()
        {
            if (_places == null || _places.Length == 0)
                return;

            Transform currentTarget = _places[_numberOfCurrentPlace];

            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, _speed * Time.deltaTime);

            if (transform.position == currentTarget.position)
                UpdateNextTarget();
        }

        private void UpdateNextTarget()
        {
            _numberOfCurrentPlace = (_numberOfCurrentPlace++) % _places.Length;

            Vector3 direction = _places[_numberOfCurrentPlace].position - transform.position;
            transform.forward = direction.normalized;
        }
    }
}

