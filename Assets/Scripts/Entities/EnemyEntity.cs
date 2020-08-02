using UnityEngine;
using Game.Wave;


namespace Game.Entities
{
    public class EnemyEntity : LivingEntity
    {
        [Header("Object References")]
        [SerializeField] private Material[] m_SnakeMaterials = new Material[0];
        [SerializeField] private GameObject m_SnakeModel = null;

        public ScoreManager scoreManager;
        [Header("Sound Effects")]
        [SerializeField] private Audio.Sound m_HitSound = null;
        protected override void Start()
        {
            base.Start();
            m_onDeath += OnDeath;

            m_SnakeModel.GetComponent<SkinnedMeshRenderer>().materials = new Material[] {
                m_SnakeMaterials[Random.Range(0, m_SnakeMaterials.Length)]
            };
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
                Audio.AudioManager.Instance.PlaySound(m_HitSound.GetClip(), gameObject.transform.position);
                scoreManager.AddScore(1);
                collision.gameObject.GetComponent<PlayerEntity>().TakeDamage(1);
                OnDeath();
            }
        }
    }
}
