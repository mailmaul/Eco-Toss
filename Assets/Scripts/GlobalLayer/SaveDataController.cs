using System.Collections;
using System.Collections.Generic;
using System.IO;
using EcoTeam.EcoToss.Audio;
using UnityEngine;

namespace EcoTeam.EcoToss.SaveData
{
    [System.Serializable]
    public class SaveData
    {
        public int SavedHighScore;
        public AudioData AudioData;
    }

    public class SaveDataController : MonoBehaviour
    {
        public static SaveDataController Instance;
        public SaveData SavedData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(base.gameObject);
            }
            else
            {
                Destroy(base.gameObject);
            }

            Load();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(SavedData);
            File.WriteAllText(Application.dataPath + "/JSON/SavedData.json", json);
            PlayerPrefs.SetString("SaveData", json);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("SaveData"))
            {
                string json = PlayerPrefs.GetString("SaveData");
                JsonUtility.FromJsonOverwrite(json, SavedData);
                File.WriteAllText(Application.dataPath + "/JSON/SavedData.json", json);
            }
            else
            {
                SavedData = new SaveData();
                SavedData.SavedHighScore = 0;
                SavedData.AudioData = Resources.Load<AudioData>("Data/AudioData");
                Save();
            }
        }

        public void SetAudioData(AudioData data)
        {
            SavedData.AudioData = data;
            Save();
        }
    }
}