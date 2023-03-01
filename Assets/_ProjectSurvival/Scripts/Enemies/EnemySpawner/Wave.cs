using System.Collections.Generic;

namespace _ProjectSurvival.Scripts.Enemies.EnemySpawner
{
    [System.Serializable]
    public class Wave 
    {
        public float TimerSecondsThisWave;
        public List<Batches> Batches;
    }
}