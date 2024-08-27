using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 6;

    public event Action<Cube> Died;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Color _defaultcolor;
    private bool _isAlive = true;

    private void OnValidate()
    {
        if (_maxLifeTime < _minLifeTime)
            _maxLifeTime = _minLifeTime;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultcolor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isAlive && collision.gameObject.TryGetComponent<DeadlyPlatform>(out _))
            Release();
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Release()
    {
        StartCoroutine(KillAfterTime());
    }

    private IEnumerator KillAfterTime()
    {
        float lifeTime = Mathf.Lerp(_minLifeTime, _maxLifeTime, Random.value);

        ChangeColourToRandom();
        _isAlive = false;

        yield return new WaitForSeconds(lifeTime);

        ChangeColorToDefault();
        _isAlive = true;
        Died?.Invoke(this);
    }

    private void ChangeColourToRandom()
    {
        if (_renderer != null)
        {
            _renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    private void ChangeColorToDefault()
    {
        _renderer.material.color = _defaultcolor;
    }
}
