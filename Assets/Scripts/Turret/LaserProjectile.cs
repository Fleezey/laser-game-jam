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

        [SerializeField] private LayerMask m_CollisionLayers;

        [Header("Sound Effects")]
        [SerializeField] private Audio.Sound m_ReflectSounds = null;

        [Header("Projectile Properties")]
        [SerializeField] private float m_DurationTime;
        [SerializeField] private float m_TravelSpeed;
        [SerializeField] private float m_Damage;

        private Camera m_Camera;

        private float m_DurationLeft;


        private void Start()
        {
            m_Camera = Camera.main;
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
                if (hit.transform.gameObject.CompareTag("Shield"))
                {
                    HandleShieldCollision(traveledDistance, hit);
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Bouncable"))
                {
                    HandleBouncableCollision(traveledDistance, rayDirection, hit);
                }
                else if (hit.transform.gameObject.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<PlayerEntity>().TakeDamage(1);
                    Destroy(gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.GetComponent<EnemyEntity>().TakeDamage(1);
                    ScoreManager.Instance.AddScore(1);
                    Destroy(gameObject);
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                {
                    Destroy(gameObject);
                }
                else
                {
                    HandleNoCollision(traveledDistance);
                }
            }
            else
            {
                HandleNoCollision(traveledDistance);
            }

            DistanceTraveled += traveledDistance;
        }

        private void HandleNoCollision(float traveledDistance)
        {
            gameObject.transform.position += gameObject.transform.forward * traveledDistance;
        }

        private void HandleShieldCollision(float traveledDistance, RaycastHit hit)
        {
            float distanceLeft = traveledDistance - Vector3.Distance(hit.point, transform.position);
            Vector3 mousePos = Input.mousePosition;
            Ray mouseCast = m_Camera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouseCast, out var mouseHit, 100))
            {
                Vector3 reflectionStartPos = hit.transform.gameObject.GetComponentInParent<Shield>().ReflectionPosition.position;
                reflectionStartPos.y = hit.point.y;
                Vector3 correctedPosition = new Vector3(mouseHit.point.x, hit.point.y, mouseHit.point.z);
                Vector3 correctedDirection = (correctedPosition - reflectionStartPos).normalized;
                ReflectionUtils.ReflectionPoint reflectedPoint = ReflectionUtils.CalculateReflectedPoint(reflectionStartPos, correctedDirection, distanceLeft);

                UpdatedTransform(reflectedPoint);

                PlayCollisionSound(hit.point);
            }
        }

        private void HandleBouncableCollision(float traveledDistance, Vector3 rayDirection, RaycastHit hit)
        {
            float distanceLeft = traveledDistance - Vector3.Distance(hit.point, transform.position);
            ReflectionUtils.ReflectionPoint reflectedPoint = ReflectionUtils.CalculateReflectedPoint(hit.point, rayDirection, hit.normal, distanceLeft);

            UpdatedTransform(reflectedPoint);

            PlayCollisionSound(hit.point);
        }

        private void UpdatedTransform(ReflectionUtils.ReflectionPoint reflectedPoint)
        {
            transform.position = reflectedPoint.m_Position;
            transform.LookAt(reflectedPoint.m_Direction);
        }

        private void PlayCollisionSound(Vector3 soundPosition)
        {
            Audio.AudioManager.Instance.PlaySound(m_ReflectSounds.GetClip(), soundPosition);
        }
    }
}
