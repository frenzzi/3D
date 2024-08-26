using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeExploder1 : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minCubeForSplit = 2;
    [SerializeField] private int _maxCubeForSplit = 6;
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1.0f;
    [SerializeField] private float _explosionForce = 5.0f;
    [SerializeField] private float _explosionRadius = 2.0f;
    [SerializeField, Min(0)] private float _scaleFactor = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _changeFactor = 0.5f;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private ExplosionHandler _explosionHandler;

    private float RandomValue => Random.value;

    private void OnValidate()
    {
        if(_minCubeForSplit >= _maxCubeForSplit)
            _maxCubeForSplit = _minCubeForSplit + 1;
    }

    private void OnMouseDown()
    {
        if (RandomValue <= _splitChance)
        {
            SplitCube();
        }

        Destroy(gameObject);
    }

    private void SplitCube()
    {
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;

        int newCubesCount = Random.Range(_minCubeForSplit, _maxCubeForSplit + 1);

        for (int i = 0; i < newCubesCount; i++)
        {
            GameObject newCube = _spawner.Spawn(gameObject, position, scale * _scaleFactor);

            Rigidbody cubeRigidBody = newCube.GetComponent<Rigidbody>();

            _explosionHandler.AddExplosionForceTo(newCube, position, _explosionForce, _explosionRadius);

            CubeExploder1 explodeer = newCube.GetComponent<CubeExploder1>();
            explodeer.ChangeSplitChance(_splitChance * _changeFactor);
        }
    }

    private void ChangeSplitChance(float newChance)
    {
        _splitChance = newChance;
    }
}
