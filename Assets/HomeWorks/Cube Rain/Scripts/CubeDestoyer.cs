using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CubeDestoyer : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 6;

    public UnityAction<CubeDestoyer> Died;

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
        _renderer = GetComponent<Renderer>();
        _defaultcolor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.GetComponent<DeadlyPlatform>() == null)
            return;

        if (_isAlive)
            Release();
    }

    private void Release()
    {
        StartCoroutine(KillAfterTime());
    }

    private IEnumerator KillAfterTime()
    {
        float elapseTime = 0f;
        float lifeTime = Mathf.Lerp(_minLifeTime, _maxLifeTime, Random.value);

        ChangeColourToRandom();
        _isAlive = false;

        while (elapseTime < lifeTime)
        {
            elapseTime += Time.deltaTime;

            yield return null;
        }

        ChangeColotToDefault();
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

    private void ChangeColotToDefault()
    {
        _renderer.material.color = _defaultcolor;
    }
}
