using System.Collections.Generic;
using _ProjectSurvival.Scripts.Enemies.Types;

namespace _ProjectSurvival.Scripts.Enemies.EnemySpawner
{
    [System.Serializable]
    public class Wave 
    {
        public float TimerSecondsThisWave;
        public List<Batches> Batches;

        public EnemyTypeSO[] GetWaveEnemies()
        {
            //Can be done more perfomant via arrays
            List<EnemyTypeSO> waveEnemies = new List<EnemyTypeSO>();
            for (int i = 0; i < Batches.Count; i++)
            {
                waveEnemies.AddRange(Batches[i].EnemiesToSpawnThisWave);
            }
            return waveEnemies.ToArray();
        }
    }
}