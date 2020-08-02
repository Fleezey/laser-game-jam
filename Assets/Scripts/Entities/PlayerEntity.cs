using UnityEngine;

namespace Game.Entities
{
    public class PlayerEntity : LivingEntity
    {
        protected override void Start()
        {
            base.Start();
            m_onDeath += OnDeath;
        }

        private void OnDeath()
        {
            GameManager.Instance.GameOver();
        }
    }
}
