using UnityEngine;

namespace Game.Entities
{
    public class EnemyEntity : LivingEntity
    {
        protected override void Start()
        {
            base.Start();
            m_onDeath += OnDeath;
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
