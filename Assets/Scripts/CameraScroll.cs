using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
     public float minFov = 15f;
     public float maxFov= 50f;
     public float sensitivity = 10f;

    

    public float fov;

    public float startFov;

    private void Start()
    {
        startFov = Camera.main.fieldOfView;
    }
    //fov = field of view
    void Update()
    {
        fov = Camera.main.fieldOfView;

        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
