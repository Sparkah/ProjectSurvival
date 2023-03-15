using UnityEngine;

namespace _ProjectSurvival.Scripts.SceneManagement
{
    /// <summary>
    /// Base level definition.
    /// </summary>
    [CreateAssetMenu(fileName = "New Game Level", menuName = "Core/Scene management/Level", order = 1)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private string _levelTitle;
        [SerializeField] private int _levelBuildIndex;

        /// <summary>
        /// Descriptive level title.
        /// </summary>
        public string LevelTitle => _levelTitle;
        /// <summary>
        /// Level build index.
        /// </summary>
        public int LevelBuildIndex => _levelBuildIndex;
    }
}
