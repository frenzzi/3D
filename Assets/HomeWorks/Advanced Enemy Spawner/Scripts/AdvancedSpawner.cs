using System.Collections;
using System.Linq;
using UnityEngine;

public class AdvancedSpawner : MonoBehaviour
{
    [SerializeField] private AdvancedTarget[] _targets;
    [SerializeField] private AdvancedEnemy _enemyPrefab;
    [SerializeField, Min(1)] private int _targetsCount = 1;
    [SerializeField] private float _spawnRate = 1.0f;

    public Vector3 SpawnPosition => transform.position;

    private bool isInitializationDone;

    private void OnValidate()
    {
        if (_targetsCount > _targets.Length)
        {
            _targetsCount = _targets.Length;
        }
    }

    private void Start()
    {
        StartCoroutine(Repeater());
    }

    private void Spawn()
    {
        AdvancedEnemy enemy = Instantiate(_enemyPrefab, SpawnPosition, Quaternion.identity);
        enemy.Initialize(GetEnemyWay());
    }

    private IEnumerator Repeater()
    {
        while (isActiveAndEnabled)
        {
            Spawn();

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private AdvancedTarget[] GetEnemyWay()
    {
        AdvancedTarget[] shuffledTargets = _targets.OrderBy(x => UnityEngine.Random.value).ToArray();

        AdvancedTarget[] enemyWay = shuffledTargets.Take(_targetsCount).ToArray();

        return enemyWay;
    }
}
