using UnityEditor;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
#if UNITY_EDITOR
    
    public class ToolsMenu : MonoBehaviour
    {
        private const string MenuScenePath = "Assets/Scenes/MenuScene.unity";
        private const string HordeScenePath = "Assets/Scenes/HordeScene.unity";

        [MenuItem("Survival Tools/Drop State")]
        static void Init()
        {
            GameProgressHandler.DeleteFile();
        }

        [MenuItem("Survival Tools/Scenes/Menu")]
        static void LoadMenu()
        {
            TryToOpenScene(MenuScenePath);
        }

        [MenuItem("Survival Tools/Scenes/Horde")]
        static void LoadHorde()
        {
            TryToOpenScene(HordeScenePath);
        }


        private static void TryToOpenScene(string scenePath)
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
        }
    }
#endif
}