using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    private void FixedUpdate()
    {
        Vector3 updatePosition = transform.position;
        updatePosition.x += _speed * Time.fixedDeltaTime;
        transform.position = updatePosition;
    }
}
