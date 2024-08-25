using System;
using UnityEngine;

public class CubeExploder1 : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minCubeForSplit = 2;
    [SerializeField] private int _maxCubeForSplit = 6;
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1.0f;
    [SerializeField] private float _explosionForce = 5.0f;
    [SerializeField] private float _explosionRadius = 2.0f;
    [SerializeField] private GameObject _cubePrefab;

    private float randomValue => UnityEngine.Random.value;

    private void OnValidate()
    {
        if(_minCubeForSplit >= _maxCubeForSplit)
            _maxCubeForSplit = _minCubeForSplit + 1;
    }

    private void OnMouseDown()
    {
        if (randomValue <= _splitChance)
        {
            SplitCube();
        }

        Destroy(gameObject);
    }

    private void SplitCube()
    {
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;

        int newCubesCount = UnityEngine.Random.Range(_minCubeForSplit, _maxCubeForSplit + 1);

        for (int i = 0; i < newCubesCount; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab, position, Quaternion.identity);

            newCube.transform.localScale = scale / 2.0f;

            Renderer cubeRenderer = newCube.GetComponent<Renderer>();
            cubeRenderer.material.color = new Color(randomValue, randomValue, randomValue);

            Rigidbody rb = newCube.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }

            CubeExploder1 explorer = newCube.GetComponent<CubeExploder1>();
            explorer._splitChance = _splitChance / 2.0f;
        }
    }
}
