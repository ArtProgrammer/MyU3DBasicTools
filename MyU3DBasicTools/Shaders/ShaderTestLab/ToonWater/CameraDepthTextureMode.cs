﻿using UnityEngine;

public class CameraDepthTextureMode : MonoBehaviour
{
    [SerializeField]
    DepthTextureMode DepthTexMode;

    private void OnValidate()
    {
        SetCameraDepthTextureMode();
    }

    private void Awake()
    {
        SetCameraDepthTextureMode();
    }

    private void SetCameraDepthTextureMode()
    {
        GetComponent<Camera>().depthTextureMode = DepthTexMode;
    }
}
