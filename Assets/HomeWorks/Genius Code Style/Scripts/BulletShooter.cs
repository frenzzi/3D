using System.Collections;
using UnityEngine;

namespace CodeStyleGenuis
{
    public class BulletShooter : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _speed = 1.0f;
        [SerializeField, Min(0)] private float _timeToShoot = 1.0f;
        [SerializeField] private Rigidbody _bulletPrefab;
        [SerializeField] private Transform _bulletTarget;

        private void Start()
        {
            StartCoroutine(Shooting());
        }

        private IEnumerator Shooting()
        {
            var wait = new WaitForSeconds(_timeToShoot);

            while (isActiveAndEnabled)
            {
                var direction = (_bulletTarget.position - transform.position).normalized;

                var newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

                newBullet.transform.up = direction;
                newBullet.velocity = direction * _speed;

                yield return wait;
            }
        }
    }
}

