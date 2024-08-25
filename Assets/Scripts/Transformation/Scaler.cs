using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Vector3 _scalingSpeed;

    private void Update()
    {
        transform.localScale += _scalingSpeed * Time.deltaTime;
    }
}
