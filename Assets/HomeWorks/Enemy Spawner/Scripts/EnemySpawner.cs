using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnRate = 1.0f;

    public Vector3 SpawnPosition => transform.position;
   
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.0f, _spawnRate);
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab, SpawnPosition, Quaternion.identity);
        Vector3 direction = GetRandomDirection();
        enemy.Initialize(direction);
    }

    private Vector3 GetRandomDirection()
    {
        float directionX = Random.Range(-1.0f, 1.0f);
        float directionY = Random.Range(-1.0f, 1.0f);
        float directionZ = Random.Range(-1.0f, 1.0f);

        Vector3 direction = new Vector3(directionX, directionY, directionZ);
        
        return direction.normalized;
    }
}
