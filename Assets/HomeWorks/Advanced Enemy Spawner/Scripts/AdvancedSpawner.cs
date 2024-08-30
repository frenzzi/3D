using System.Collections;
using UnityEngine;

public class AdvancedSpawner : MonoBehaviour
{
    [SerializeField] private AdvancedTarget _target;
    [SerializeField] private AdvancedEnemy _enemyPrefab;
    [SerializeField, Min(0)] private float _spawnRate = 1.0f;

    public Vector3 SpawnPosition => transform.position;


    private void Start()
    {
        StartCoroutine(Repeater());
    }

    private void Spawn()
    {
        AdvancedEnemy enemy = Instantiate(_enemyPrefab, SpawnPosition, Quaternion.identity);
        enemy.Initialize(_target);
    }

    private IEnumerator Repeater()
    {
        while (isActiveAndEnabled)
        {
            Spawn();

            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
