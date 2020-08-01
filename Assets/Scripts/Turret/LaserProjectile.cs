using Game.Entities;
using Game.Utils;

using System.Collections.Generic;
using UnityEngine;


namespace Game.Turrets
{
    public class LaserProjectile : MonoBehaviour
    {
        public LayerMask CollisionLayers => m_CollisionLayers;
        public float DistanceTraveled { get; private set; }

        [SerializeField] private float m_DurationTime;
        [SerializeField] private float m_TravelSpeed;
        [SerializeField] private float m_Damage;
        [SerializeField] private LayerMask m_CollisionLayers;

        [Header("Sound Effects")]
        [SerializeField] private Audio.Sound m_ReflectSounds = null;

        private float m_DurationLeft;


        private void Start()
        {
            m_DurationLeft = m_DurationTime;
            DistanceTraveled = 0f;
        }

        private void Update()
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
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Bouncable"))
                {
                    float distanceLeft = traveledDistance - Vector3.Distance(hit.point, transform.position);
                    ReflectionUtils.ReflectionPoint reflectedPoint = ReflectionUtils.CalculateReflectedPoint(hit.point, rayDirection, hit.normal, distanceLeft);
                    
                    transform.position = reflectedPoint.m_Position;
                    transform.LookAt(reflectedPoint.m_Direction);

                    Audio.AudioManager.Instance.PlaySound(m_ReflectSounds.GetClip(), hit.point);
                }
                else if (hit.transform.gameObject.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<PlayerEntity>().TakeDamage(1);
                    Destroy(gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.GetComponent<EnemyEntity>().TakeDamage(1);
                    Destroy(gameObject);
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Vector3 newPosition = transform.position + transform.forward * traveledDistance;
                gameObject.transform.position += gameObject.transform.forward * traveledDistance;
            }

            DistanceTraveled += traveledDistance;
        }
    }
}
