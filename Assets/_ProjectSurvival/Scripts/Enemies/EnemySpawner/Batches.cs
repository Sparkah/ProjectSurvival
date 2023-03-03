using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.EnemySpawner
{
    [System.Serializable]
    public class Batches
    {
        public EnemyTypeSO[] EnemiesToSpawnThisWave;
        public float SpawnSpeedSecondsThisWave;
        public int SpawnAmountThisWave;
    }
}
