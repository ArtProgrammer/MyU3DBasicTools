using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public interface ITriggerRegion
    {
        bool IsTouching(Vector3 pos, float radius);
    }

    public class TriggerRegion_Circle : ITriggerRegion
    {
        private Vector3 Pos = Vector3.zero;

        private float Radius;

        public TriggerRegion_Circle(Vector3 pos, float radius)
        {
            Pos = pos;
            Radius = radius;
        }

        public bool IsTouching(Vector3 pos, float radius)
        {
            var dir = pos - Pos;
            return dir.sqrMagnitude < (Radius + radius) * (Radius + radius);
        }
    }

    public class TriggerRegion_Rectangle : ITriggerRegion
    {
        private Bounds TriggerBounds;

        public TriggerRegion_Rectangle(Vector3 center, Vector3 extents)
        {
            TriggerBounds.center = center;
            TriggerBounds.extents = extents;
        }

        public bool IsTouching(Vector3 pos, float radius)
        {
            Bounds bounds = new Bounds(pos, new Vector3(radius, radius, radius));

            return TriggerBounds.Intersects(bounds);
        }
    }
}
