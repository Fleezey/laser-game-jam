using Game.Utils;

using System.Collections.Generic;
using UnityEngine;


namespace Game.Turrets
{
    [RequireComponent(typeof (LineRenderer), typeof (LaserProjectile))]
    public class LaserTrail : MonoBehaviour
    {
        [SerializeField] private int m_PointsPerSegment = 10;
        [SerializeField] private float m_LaserLength = 20f;

        private LaserProjectile m_LaserProjectile;
        private LineRenderer m_LineRenderer;


        private void Awake()
        {
            m_LaserProjectile = GetComponent<LaserProjectile>();
            m_LineRenderer = GetComponent<LineRenderer>();    
        }

        private void Update()
        {
            float laserLength = Mathf.Clamp(m_LaserLength, 0, m_LaserProjectile.DistanceTraveled);
            List<ReflectionUtils.ReflectionSegment> segments = ReflectionUtils.ReflectRay(transform.position, transform.forward * -1, laserLength, m_LaserProjectile.CollisionLayers);

            m_LineRenderer.SetPositions(GetPoints(segments));
        }

        private Vector3[] GetPoints(List<ReflectionUtils.ReflectionSegment> segments)
        {
            Vector3[] points = new Vector3[m_PointsPerSegment * segments.Count];
            m_LineRenderer.positionCount = points.Length;
            int pointIndex = 0;

            segments.ForEach((ReflectionUtils.ReflectionSegment segment) => {
                float segmentLength = Vector3.Distance(segment.m_PointA, segment.m_PointB);
                float subLength = segmentLength / m_PointsPerSegment;

                Vector3 direction = (segment.m_PointB - segment.m_PointA).normalized;

                for (int i = 0; i < m_PointsPerSegment; i++)
                {
                    points[pointIndex++] = segment.m_PointA + direction * subLength * i;
                }
            });

            return points;
        }

        // private Vector3[] GetPoints(List<ReflectionUtils.ReflectionSegment> segments)
        // {
        //     Vector3[] points = new Vector3[m_PointsPerSegment * segments.Count];
        //     m_LineRenderer.positionCount = points.Length;
        //     int pointIndex = 0;

        //     for (int segmentIndex = 0; segmentIndex < segments.Count; segmentIndex++)
        //     {
        //         ReflectionUtils.ReflectionSegment segment = segments[segmentIndex];
        //         Vector3 direction = (segmentIndex % 2 == 0
        //             ? segment.m_PointB - segment.m_PointA
        //             : segment.m_PointA - segment.m_PointB).normalized;
                
        //         float segmentLength = Vector3.Distance(segment.m_PointA, segment.m_PointB);
        //         float subLength = segmentLength / m_PointsPerSegment;

        //         for (int i = 0; i < m_PointsPerSegment; i++)
        //         {
        //             points[pointIndex++] = segment.m_PointA + direction * subLength * i;
        //         }
        //     }

        //     // segments.ForEach((ReflectionUtils.ReflectionSegment segment) => {
        //     //     float segmentLength = Vector3.Distance(segment.m_PointA, segment.m_PointB);
        //     //     float subLength = segmentLength / m_PointsPerSegment;

        //     //     Vector3 direction = (segment.m_PointB - segment.m_PointA).normalized;

        //     //     for (int i = m_PointsPerSegment - 1; i >= 0; i--)
        //     //     {
        //     //         points[pointIndex--] = segment.m_PointA + direction * subLength * i;
        //     //     }
        //     // });

        //     return points;
        // }
    }
}
