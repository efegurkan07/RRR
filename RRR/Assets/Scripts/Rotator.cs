using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float RotationSpeedX = 60f;
    [SerializeField] private float RotationSpeedY = 60f;
    [SerializeField] private float RotationSpeedZ = 60f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationSpeedX, RotationSpeedY * Time.deltaTime, RotationSpeedZ, Space.World);
    }
}
