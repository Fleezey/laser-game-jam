using System;
using UnityEngine;


namespace Game.Entities
{
    public abstract class LivingEntity : MonoBehaviour
    {
        public float Health => m_Health;
        public float MaxHealth => m_MaxHealth;

        public event Action m_onDeath = delegate { };

        protected bool m_IsDead;
        protected float m_Health;
        [SerializeField] protected float m_MaxHealth;


        protected virtual void Start()
        {
            m_Health = m_MaxHealth;
        }

        public virtual void TakeHit(float damage)
        {
            // TODO: Could do something with the hit point, direction. Something like knockback
            TakeDamage(damage);
        }
        public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
        {
            // TODO: Could do something with the hit point, direction. Something like knockback
            TakeDamage(damage);
        }

        public virtual void TakeDamage(float damage)
        {
            m_Health -= damage;

            if (m_Health <= 0 && !m_IsDead)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            m_IsDead = true;

            if (m_onDeath != null)
            {
                m_onDeath();
            }
        }
    }
}
