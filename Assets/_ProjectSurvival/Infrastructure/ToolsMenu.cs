using UnityEditor;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
    public class ToolsMenu : MonoBehaviour
    {
        [MenuItem("Neural Tools/Drop State")]
        static void Init()
        {
            GameProgressHandler.DeleteFile();
        }
    }
}