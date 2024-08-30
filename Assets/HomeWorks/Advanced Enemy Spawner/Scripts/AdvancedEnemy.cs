using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed = 0.5f;

    private AdvancedTarget _target;

    private void Update()
    {
        MoveToTarget();
    }

    public void Initialize(AdvancedTarget target)
    {
        _target = target;
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
            Destroy(gameObject);
        }
    }
}
