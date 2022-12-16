using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions
{
    private const float MinAspectRatio = 1.2f;

    public static void SetWidth(this Camera camera, float width)
    {
        float aspectRatio = (float)Screen.height / Screen.width;

        if (aspectRatio < MinAspectRatio)
            width *= MinAspectRatio / aspectRatio;

        camera.orthographicSize = width * aspectRatio * 0.5f;
    }
}