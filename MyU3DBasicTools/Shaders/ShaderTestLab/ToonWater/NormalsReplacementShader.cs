using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalsReplacementShader : MonoBehaviour
{
    [SerializeField]
    Shader NormalsShader;

    private RenderTexture RT;

    private Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        Camera thisCamera = GetComponent<Camera>();

        RT = new RenderTexture(thisCamera.pixelWidth, thisCamera.pixelHeight, 24);

        Shader.SetGlobalTexture("_CameraNormalsTexture", RT);

        GameObject copy = new GameObject("Normal Camera");
        Cam = copy.AddComponent<Camera>();
        Cam.CopyFrom(thisCamera);
        Cam.transform.SetParent(transform);
        Cam.targetTexture = RT;
        Cam.SetReplacementShader(NormalsShader, "RenderType");
        Cam.depth = thisCamera.depth - 1;
    }
}
