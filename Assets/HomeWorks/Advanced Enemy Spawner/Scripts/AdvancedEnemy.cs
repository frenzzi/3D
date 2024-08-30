using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AdvancedEnemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed = 0.5f;
    
    [SerializeField] private Queue<AdvancedTarget> _way;
    [SerializeField] private AdvancedTarget _target;

    private void Start()
    {
        if (_way == null)
            return;

        if (_way.TryDequeue(out _target) == false )
            Destroy(gameObject);
    }

    private void Update()
    {
        MoveToTarget();
    }

    public void Initialize(AdvancedTarget[] way)
    {
        _way = new Queue<AdvancedTarget>(way);
    }

    private void MoveToTarget()
    {
        transform.up = _target.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _target.gameObject)
        {
            if (_way.TryDequeue(out _target) == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
