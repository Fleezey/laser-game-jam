using System;
using UnityEngine;


namespace Game.Turrets
{
    public class LaserProjectile : MonoBehaviour
    {
        [SerializeField] private float m_DurationTime;
        [SerializeField] private float m_TravelSpeed;
        [SerializeField] private LayerMask m_CollisionLayers;

        private float m_DurationLeft;

        private void Start()
        {
            m_DurationLeft = m_DurationTime;
        }

        private void LateUpdate()
        {
            m_DurationLeft -= Time.deltaTime;

            if (m_DurationLeft < 0)
            {
                Destroy(gameObject);
            }

            MoveProjectile(Time.deltaTime);
        }

        private void MoveProjectile(float fixedDeltaTime)
        {
            Vector3 rayDirection = transform.forward;
            RaycastHit hit;
            float traveledDistance = fixedDeltaTime * m_TravelSpeed;

            if (Physics.Raycast(transform.position, rayDirection, out hit, traveledDistance, m_CollisionLayers))
            {
                // Collision
                float distanceLeft = traveledDistance - Vector3.Distance(hit.point, transform.position);
                ReflectProjectile(hit, distanceLeft);
            }
            else
            {
                gameObject.transform.position += gameObject.transform.forward * traveledDistance;
            }
        }

        private void ReflectProjectile(RaycastHit hit, float distance)
        {
            Vector3 directionAfterReflection = Vector3.Reflect(gameObject.transform.forward, hit.normal).normalized;
            Vector3 newPos = hit.point + directionAfterReflection * distance;
            
            Vector3 correctedPosition = new Vector3(newPos.x, transform.position.y, newPos.z);
            Vector3 lookAtPosition = correctedPosition + (correctedPosition - hit.point).normalized * 1f;

            transform.position = correctedPosition;
            transform.LookAt(lookAtPosition);
        }
    }
}
