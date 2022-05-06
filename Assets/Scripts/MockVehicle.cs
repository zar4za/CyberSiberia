using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockVehicle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.fixedDeltaTime, 0, 0);
    }
}
