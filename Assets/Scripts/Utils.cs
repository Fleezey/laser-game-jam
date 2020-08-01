using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Utils
{
    public static class ReflectionUtils
    {
        public delegate void OnCollision();

        public static List<ReflectionSegment> ReflectRay(Vector3 position, Vector3 direction, float distance, LayerMask collisionLayers)
        {
            List<ReflectionSegment> segments = new List<ReflectionSegment>();

            RaycastHit hit;

            if (Physics.Raycast(position, direction, out hit, distance, collisionLayers))
            {
                float distanceRemaining = distance - hit.distance;
                ReflectionPoint reflectionPoint = CalculateReflectedPoint(hit.point, direction, hit.normal, distance);
                segments.Add(new ReflectionSegment(position, hit.point));
                segments.Add(new ReflectionSegment(hit.point, reflectionPoint.m_Position));
            }
            else
            {
                segments.Add(new ReflectionSegment(position, position + direction * distance));
            }

            return segments;
        }

        public static ReflectionPoint CalculateReflectedPoint(Vector3 position, Vector3 direction, Vector3 normal, float distance)
        {
            Vector3 reflectedDirection = Vector3.Reflect(direction, normal).normalized;
            Vector3 reflectedPosition = position + reflectedDirection * distance;

            Vector3 correctedPosition = new Vector3(reflectedPosition.x, position.y, reflectedPosition.z);
            Vector3 lookAtPosition = correctedPosition + (correctedPosition - position).normalized * 1f;

            return new ReflectionPoint(correctedPosition, lookAtPosition);
        }

        public static ReflectionPoint CalculateReflectedPoint(Vector3 position, Vector3 outDirection, float distance)
        {
            Vector3 reflectedPosition = position + outDirection * distance;

            Vector3 correctedPosition = new Vector3(reflectedPosition.x, position.y, reflectedPosition.z);
            Vector3 lookAtPosition = correctedPosition + (correctedPosition - position).normalized * 1f;

            return new ReflectionPoint(correctedPosition, lookAtPosition);
        }

        public struct ReflectionPoint
        {
            public Vector3 m_Position;
            public Vector3 m_Direction;

            public ReflectionPoint(Vector3 position, Vector3 direction)
            {
                m_Position = position;
                m_Direction = direction;
            }
        }

        public struct ReflectionSegment
        {
            public Vector3 m_PointA;
            public Vector3 m_PointB;

            public ReflectionSegment(Vector3 pointA, Vector3 pointB)
            {
                m_PointA = pointA;
                m_PointB = pointB;
            }
        }
    }
}

