using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum_script : MonoBehaviour
{
    public float maxAngle,
        speed;
    public Vector3 axis;

    private void Update()
    {
        float angle = Mathf.Sin(speed * Time.time) * maxAngle;
        transform.localRotation = Quaternion.Euler(axis * angle);
    }
}
