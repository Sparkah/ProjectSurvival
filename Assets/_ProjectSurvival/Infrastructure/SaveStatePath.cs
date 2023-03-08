using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
    public class SaveStatePath
    {
        public static readonly string StatePath = Application.persistentDataPath + "/config/local.state";
    }
}