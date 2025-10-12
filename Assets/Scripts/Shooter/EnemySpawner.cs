using UnityEngine;

namespace GAME360Project.Assets.Scripts.Shooter
{

    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawning")]
        public GameObject enemyPrefab;
        public float spawnRate = 2f;
        public Transform[] spawnPoints;

        private float nextSpawnTime = 0f;

        private void Update()
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnRate;
            }
            if (ShooterManager.Instance.score > 400 && ShooterManager.Instance.score < 900)
                spawnRate = 1.5f;
            if (ShooterManager.Instance.score > 900 && ShooterManager.Instance.score < 1400)
                spawnRate = 1.0f;
            if (ShooterManager.Instance.score > 1400 && ShooterManager.Instance.score < 2000)
                spawnRate = 0.5f;
        }

        private void SpawnEnemy()
        {
            if (enemyPrefab && spawnPoints.Length > 0)
            {
                // Check if game is still running through Singleton
                if (ShooterManager.Instance.lives > 0)
                {
                    int randomIndex = Random.Range(0, spawnPoints.Length);
                    Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
                }
            }
        }
    }
}