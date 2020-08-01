using System;
using UnityEngine;


namespace Game.Turrets
{
    public class LaserProjectile : SimpleProjectile
    {
        [SerializeField] private float m_DurationTime;

        private float m_DurationLeft;

        protected override void Start()
        {
            base.Start();

            m_DurationLeft = m_DurationTime;
        }

        private void FixedUpdate()
        {
            m_DurationLeft -= Time.fixedDeltaTime;

            if (m_DurationLeft < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Shield")
            {
                ReflectProjectile(other.transform.forward);
            }
        }

        private void ReflectProjectile(Vector3 inNormal)
        {
            m_Rigidbody.velocity = m_Rigidbody.velocity.magnitude* Vector3.Reflect(transform.forward, inNormal);
        }
    }
}
