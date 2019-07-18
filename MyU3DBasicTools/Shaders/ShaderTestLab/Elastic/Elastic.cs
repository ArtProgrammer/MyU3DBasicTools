using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Inputs;

namespace TestShaders
{
    public interface IElastic
    {
        void OnElastic(RaycastHit hit);
    }

    public class Elastic : MonoBehaviour, IElastic
    {
        private static int Pos, Nor, STime;

        public float RangeFactor = 0.6f;

        public float Intensity = 0.2f;

        private MeshRenderer MeshR;

        static Elastic()
        {
            Pos = Shader.PropertyToID("_Position");
            Nor = Shader.PropertyToID("_Normal");
            STime = Shader.PropertyToID("_PointTime");
        }

        // Start is called before the first frame update
        void Start()
        {
            InputKeeper.Instance.OnLeftClickHit += OnElastic;
            MeshR = GetComponent<MeshRenderer>();
        }

        public void OnElastic(RaycastHit hit)
        {
            Debug.Log("$ left click hit");
            Vector4 v = transform.InverseTransformPoint(hit.point);
            v.w = RangeFactor;
            MeshR.material.SetVector(Pos, v);
            v = transform.InverseTransformDirection(hit.normal.normalized);
            v.w = Intensity;
            MeshR.material.SetVector(Nor, v);
            MeshR.material.SetFloat(STime, Time.time);
        }
    }
}