using UnityEngine;
using Game.Wave;

namespace Game.Entities
{
    public class EnemyEntity : LivingEntity{
        public ScoreManager scoreManager;
        protected override void Start()
        {
            base.Start();
            m_onDeath += OnDeath;
        }

        private void OnDeath()
        {
            WaveManager.Instance.OnEnemyDeath();

            m_onDeath -= OnDeath;
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerEntity>().TakeHit(1);
                scoreManager.AddScore(1);
                OnDeath();
            }
        }
    }
}
