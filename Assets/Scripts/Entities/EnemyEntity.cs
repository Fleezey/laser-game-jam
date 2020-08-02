using UnityEngine;
using Game.Wave;

namespace Game.Entities
{
    public class EnemyEntity : LivingEntity{
        public ScoreManager scoreManager;
        [Header("Sound Effects")]
        [SerializeField] private Audio.Sound m_HitSound = null;
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
                Audio.AudioManager.Instance.PlaySound(m_HitSound.GetClip(), gameObject.transform.position);
                scoreManager.AddScore(1);
                OnDeath();
            }
        }
    }
}
