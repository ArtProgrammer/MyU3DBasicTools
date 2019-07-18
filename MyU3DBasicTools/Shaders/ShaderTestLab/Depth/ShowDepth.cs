using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDepth : MonoBehaviour
{
    public Material mat;

    void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat)
            Graphics.Blit(source, destination, mat);
        //mat is the material which contains the shader
        //we are passing the destination RenderTexture to
    }
}