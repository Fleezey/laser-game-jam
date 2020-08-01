using System;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerEntity : LivingEntity
    {
        public HealthBar healthBar;
        protected override void Start()
        {
            base.Start();
            healthBar.SetMaxHealth(m_MaxHealth);
            m_onDeath += OnDeath;
        }
        public override void TakeHit(float damage)
        {
            base.TakeHit(damage);
            healthBar.SetHealth(m_Health);
        }

        private void OnDeath()
        {
            Debug.Log("Game End");
        }
    }
}
