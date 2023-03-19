using UnityEngine.SceneManagement;

namespace _ProjectSurvival.Scripts.SceneManagement
{
    /// <summary>
    /// Level loading behaviour.
    /// </summary>
    public class LevelLoader
    {
        private const string _menuSceneName = "MenuScene";
        private static int _currentSceneIndex = -1;

        /// <summary>
        /// Load selected level.
        /// </summary>
        /// <param name="level">Selected level scriptable object.</param>
        public void LoadLevel(LevelSO level)
        {
            if (level)
            {
                LoadScene(level.LevelBuildIndex);
            }
        }

        /// <summary>
        /// Reload current level.
        /// </summary>
        public void ReloadCurrentLevel()
        {
            if (_currentSceneIndex != -1)
                LoadScene(_currentSceneIndex);
        }

        public void LoadNextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (IsSceneExist(nextSceneIndex))
                LoadScene(nextSceneIndex);
            else
                LoadScene(_menuSceneName);
        }

        private void LoadScene(int buildIndex)
        {
            if (IsSceneExist(buildIndex))
            {
                ChangeCurrentScene(buildIndex);
                SceneManager.LoadScene(buildIndex);
            }
        }

        private void LoadScene(string sceneName)
        {
            ChangeCurrentScene(sceneName);
            SceneManager.LoadScene(sceneName); //Can be switched to async for loading screen
        }

        private bool IsSceneExist(int buildIndex)
        {
            return buildIndex >= 0 && buildIndex < SceneManager.sceneCountInBuildSettings;
        }

        private void ChangeCurrentScene(string sceneName)
        {
            _currentSceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        }

        private void ChangeCurrentScene(int buildIndex)
        {
            _currentSceneIndex = buildIndex;
        }
    }
}
