using UnityEngine;

public class AdvancedTarget : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField, Min(0)] private float _speed;

    private bool isStartPosition => transform.position == _startPosition.position;
    private bool isEndPosition => transform.position == _endPosition.position;

    private bool isMoveToStart = false;

    private void Awake()
    {
        transform.position = _startPosition.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isMoveToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition.position, _speed * Time.deltaTime);

            if (isStartPosition)
            {
                isMoveToStart = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition.position, _speed * Time.deltaTime);

            if (isEndPosition)
            {
                isMoveToStart = true;
            }
        }
    }
}
