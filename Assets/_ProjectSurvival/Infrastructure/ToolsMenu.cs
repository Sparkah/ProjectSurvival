using UnityEditor;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
#if UNITY_EDITOR
    
    public class ToolsMenu : MonoBehaviour
    {
        [MenuItem("Survival Tools/Drop State")]
        static void Init()
        {
            GameProgressHandler.DeleteFile();
        }
    }
#endif
}