﻿using System;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerEntity : LivingEntity
    {
        [Header("Sound Effects")]
        [SerializeField] private Audio.Sound m_HitSound = null;
        
        public HealthBar healthBar;
        protected override void Start()
        {
            base.Start();
            healthBar.SetMaxHealth(m_MaxHealth);
            m_onDeath += OnDeath;
        }
        public override void TakeDamage(float damage)
        {
            Audio.AudioManager.Instance.PlaySound(m_HitSound.GetClip(), gameObject.transform.position);
            base.TakeDamage(damage);
            healthBar.SetHealth(m_Health);
        }

        private void OnDeath()
        {
            GameManager.Instance.GameOver();
        }
    }
}
