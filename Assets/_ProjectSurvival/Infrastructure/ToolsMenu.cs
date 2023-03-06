using UnityEditor;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
    public class ToolsMenu : MonoBehaviour
    {
        [MenuItem("Survival Tools/Drop State")]
        static void Init()
        {
            GameProgressHandler.DeleteFile();
        }
    }
}