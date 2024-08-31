using System.Collections;
using UnityEngine;

namespace AdvancedSpawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Target _target;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField, Min(0)] private float _spawnRate = 1.0f;

        private void Start()
        {
            StartCoroutine(Repeating());
        }

        private void Spawn()
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            enemy.Initialize(_target);
        }

        private IEnumerator Repeating()
        {
            while (isActiveAndEnabled)
            {
                Spawn();

                yield return new WaitForSeconds(_spawnRate);
            }
        }
    }
}

