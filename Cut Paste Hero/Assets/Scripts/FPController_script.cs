using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController_script : MonoBehaviour
{
    public Camera cam;
    public float minPitch,
        maxPitch,
        cameraSpeed,
        walkSpeed;
    public bool canMove,
        canInput;

    private void CameraLook(float x, float y)
    {
        Vector2 rotation = new Vector2(x, y);

        if (cam.transform.rotation.x + rotation.y < minPitch)
        {
            float difference = minPitch - (cam.transform.rotation.x + rotation.y);
            rotation.y += difference;
        }
        else if (cam.transform.rotation.x + rotation.y > maxPitch)
        {
            float difference = (cam.transform.rotation.x + rotation.y) - maxPitch;
            rotation.y -= difference;
        }

        transform.Rotate(0f, x, 0f);
        cam.transform.Rotate(y, 0f, 0f);
    }

    private void Update()
    {
        if (canInput)
        {
            CameraLook(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        }
    }
}
