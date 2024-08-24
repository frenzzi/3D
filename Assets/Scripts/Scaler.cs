using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Vector3 _scalingSpeed;

    private Vector3 _normalScale;
    private Vector3 _currentSpeed;
    private bool isScalingUp = true;

    private void Start()
    {
        _normalScale = transform.localScale;
        _currentSpeed = _scalingSpeed;
    }

    private void Update()
    {
        Scale();
    }

    private void Scale()
    {
        transform.localScale += _currentSpeed * Time.deltaTime;

        if (isScalingUp)
        {
            if (IsMaxScale())
            {
                isScalingUp = false;
                _currentSpeed = -_scalingSpeed;
            }
        }
        else
        {
            if(IsNormalScale())
            {
                isScalingUp = true;
                _currentSpeed = _scalingSpeed;
            }
        }

        transform.localScale = new Vector3(
            Mathf.Clamp(transform.localScale.x, _normalScale.x, _maxScale.x),
            Mathf.Clamp(transform.localScale.y, _normalScale.y, _maxScale.y),
            Mathf.Clamp(transform.localScale.z, _normalScale.z, _maxScale.z)
            );
    }

    private bool IsNormalScale()
    {
        bool isNormalScaleX = transform.localScale.x <= _normalScale.x;
        bool isNormalScaleY = transform.localScale.y <= _normalScale.y;
        bool isNormalScaleZ = transform.localScale.z <= _normalScale.z;

        return isNormalScaleX && isNormalScaleY && isNormalScaleZ;
    }

    private bool IsMaxScale()
    {
        bool isNormalScaleX = transform.localScale.x >= _maxScale.x;
        bool isNormalScaleY = transform.localScale.y >= _maxScale.y;
        bool isNormalScaleZ = transform.localScale.z >= _maxScale.z;

        return isNormalScaleX && isNormalScaleY && isNormalScaleZ;
    }
}
