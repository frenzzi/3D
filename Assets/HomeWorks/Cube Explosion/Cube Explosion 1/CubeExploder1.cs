using UnityEngine;
using CubeExploder;

[RequireComponent(typeof(Rigidbody))]
public class CubeExploder1 : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minCubeForSplit = 2;
    [SerializeField] private int _maxCubeForSplit = 6;
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1.0f;
    [SerializeField, Min(0)] private float _scaleFactor = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _changeFactor = 0.5f;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private ExplosionHandler _explosionHandler;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody
    {
        get
        {
            return _rigidbody;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(float newChance)
    {
        _splitChance = newChance;
    }

    private void OnValidate()
    {
        if (_minCubeForSplit >= _maxCubeForSplit)
            _maxCubeForSplit = _minCubeForSplit + 1;
    }

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
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
            CubeExploder1 newCubeExploder = _spawner.Spawn(this, position, scale * _scaleFactor);

            _explosionHandler.AddExplosionForceTo(newCubeExploder.Rigidbody, position);

            newCubeExploder.Initialize(_splitChance * _changeFactor);
        }
    }
}




























