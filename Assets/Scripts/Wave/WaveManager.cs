using Boo.Lang;
using System.Collections;
using UnityEngine;


namespace Game.Wave
{
    public class WaveManager : Singleton<WaveManager>
    {
        [Header("References")]
        [SerializeField] public Transform[] m_SpawningPoint = null;
        [SerializeField] private GameObject m_Enemy = null;

        [Header("Wave Properties")]
        [SerializeField] private uint m_EnemiesTotalCount = 0;
        [SerializeField] private float m_SpawnDelay = 0;


        private void Start()
        {
            float cumulativeDelay = 0.0f;
            for(uint i = 0; i < m_EnemiesTotalCount; ++i)
            {
                int spawnIndex = Random.Range(0, m_SpawningPoint.Length);
                StartCoroutine(DelayedSpawnEnemy(cumulativeDelay, m_SpawningPoint[spawnIndex].position));
                cumulativeDelay += m_SpawnDelay;
            }
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
