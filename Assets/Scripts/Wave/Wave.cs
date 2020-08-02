namespace Game.Wave
{
    public class Wave
    {
        public int EnemiesTotalCount { get; private set; } = 0;
        public float SpawnDelay { get; private set; } = 0;

        public Wave(int enemiesTotalCount, float spawnDelay)
        {
            EnemiesTotalCount = enemiesTotalCount;
            SpawnDelay = spawnDelay;
        }
    }
}
