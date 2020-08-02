using System.Collections;
using UnityEngine;


namespace Game.Wave
{
    public class WaveManager : Singleton<WaveManager>
    {
        [Header("References")]
        [SerializeField] private GameObject m_Enemy = null;

        [Header("Wave Properties")]
        [SerializeField] private int m_InitialWaveEnemyCount = 0;
        [SerializeField] private int m_InitialWaveSpawnDelay = 0;
        [SerializeField] private int m_EnemyCountDelayIncrement = 0;
        [SerializeField] private int m_WaveSpawnDelayDecrement = 0;

        public Wave CurrentWave { get; private set; } = null;
        public bool WaveCompleted { get; private set; } = true;

        private GameObject[] m_SpawningPoint = null;
        private int m_EnemyDeathCount = 0;


        private void Start()
        {
            CurrentWave = new Wave(m_InitialWaveEnemyCount, m_InitialWaveSpawnDelay);
            m_SpawningPoint = GameObject.FindGameObjectsWithTag("Spawning Point");
        }

        private void Update()
        {
            if (m_EnemyDeathCount == CurrentWave.EnemiesTotalCount)
            {
                m_EnemyDeathCount = 0;
                OnCurrentWaveCleared();
            }
        }

        public void StartWave()
        {
            if (!WaveCompleted)
                return;

            StartWave(CurrentWave);
        }

        public void StartWave(Wave wave)
        {
            if (!WaveCompleted)
                return;

            if (CurrentWave != wave)
                CurrentWave = wave;

            StartCurrentWave();
        }

        public void OnEnemyDeath()
        {
            Debug.Log("Killed an enemy");
            ++m_EnemyDeathCount;
        }

        private void StartCurrentWave()
        {
            float cumulativeDelay = 0.0f;
            for (uint i = 0; i < CurrentWave.EnemiesTotalCount; ++i)
            {
                int spawnIndex = Random.Range(0, m_SpawningPoint.Length);
                StartCoroutine(DelayedSpawnEnemy(cumulativeDelay, m_SpawningPoint[spawnIndex].transform.position));
                cumulativeDelay += CurrentWave.SpawnDelay;
            }
        }

        private void OnCurrentWaveCleared()
        {
            WaveCompleted = true;
            CurrentWave = new Wave(CurrentWave.EnemiesTotalCount + m_EnemyCountDelayIncrement,
                Mathf.Max(0, CurrentWave.SpawnDelay - m_WaveSpawnDelayDecrement));

            Debug.Log("NEW WAVE INCOMING");
            StartCurrentWave();
        }

        private IEnumerator DelayedSpawnEnemy(float delay, Vector3 spawnPosition)
        {
            yield return new WaitForSeconds(delay);
            SpawnEnemy(spawnPosition);
        }

        private GameObject SpawnEnemy(Vector3 spawnPosition)
        {
            return Instantiate(m_Enemy, spawnPosition, Quaternion.identity);
        }
    }
}
