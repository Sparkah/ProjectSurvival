using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
    public class GameProgressHandler : MonoBehaviour
    {
        [SerializeField] private bool _encryptData = true;
        private World _world;
        private float _currentTime;

        private void Start()
        {
            InitWorld();
        }

        private void OnDestroy()
        {
            SaveGameActions();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            SaveGameActions();
        }

        private void OnApplicationQuit()
        {
            SaveGameActions();
        }
        
        public World InitWorld()
        {
            if (File.Exists(SaveStatePath.StatePath))
            {
                using var streamReader = new StreamReader(SaveStatePath.StatePath);
                try
                {
                    string loadedState = _encryptData
                        ? EncryptDecrypt.EncryptDecryptData(streamReader.ReadToEnd())
                        : streamReader.ReadToEnd();
                    
                    _world = JsonFx.Json.JsonReader.Deserialize<World>(loadedState);
                }
                catch (Exception)
                {
                    _world = new World();
                }
            }
            else
            {
                _world = new World();
            }

            return _world;
        }

        public static void DeleteFile()
        {
            File.Delete(SaveStatePath.StatePath);
        }

        private void SaveGameActions()
        {
            CreateFolderIfNotExists();
            Save();
        }

        private void Save()
        {
            if (_world == null)
                return;

            string json = JsonFx.Json.JsonWriter.Serialize(_world);
            WriteToFile(_encryptData ? EncryptDecrypt.EncryptDecryptData(json) : json);
        }

        private void CreateFolderIfNotExists()
        {
            try
            {
                string folderPath = Path.GetDirectoryName(SaveStatePath.StatePath);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        private static void WriteToFile(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;

            using var sw = new StreamWriter(SaveStatePath.StatePath, false, Encoding.UTF8);
            sw.Write(data);
        }
    }
}