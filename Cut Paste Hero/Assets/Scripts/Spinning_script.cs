using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning_script : MonoBehaviour
{
    public float speed;
    public Vector3 axis;

    private void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime);
    }
}
