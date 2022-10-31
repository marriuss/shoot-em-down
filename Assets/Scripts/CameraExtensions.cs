using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions
{
    public static Bounds GetOrthographicBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        return new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }
}