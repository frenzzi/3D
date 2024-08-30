using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed = 0.5f;

    private Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    private void Start()
    {
        RotateToDirection();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void RotateToDirection()
    {
        Quaternion rotation = Quaternion.LookRotation(_direction);
        transform.rotation = rotation;
    }
}
