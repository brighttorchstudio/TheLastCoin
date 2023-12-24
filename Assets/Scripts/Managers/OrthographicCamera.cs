using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthographicCamera : MonoBehaviour
{
    public SpriteRenderer rink;
    private float screenRatio;
    private float targetRatio;
    private void Start()
    {
        screenRatio = (float)Screen.width / (float)Screen.height;
        targetRatio = (float)rink.bounds.size.x / (float)rink.bounds.size.y;
        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = rink.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = rink.bounds.size.y / 2 * differenceInSize;
        }
    }

    private void Update()
    {
        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = rink.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = rink.bounds.size.y / 2 * differenceInSize;
        }
    }
}
