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
    }

    public class SaveDataController : MonoBehaviour
    {
        public static SaveDataController Instance;
        public SaveData SaveData;
        public AudioData AudioData;
        private readonly string[] DataLibrary = {
            "SaveData",
            "AudioData",
        };

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
            string jsonSaveData = JsonUtility.ToJson(SaveData);
            string jsonAudioData = JsonUtility.ToJson(AudioData);
            File.WriteAllText(Application.dataPath + "/SavedData.json", jsonSaveData);
            File.WriteAllText(Application.dataPath + "/SavedAudioData.json", jsonAudioData);
            PlayerPrefs.SetString(DataLibrary[0], jsonSaveData);
            PlayerPrefs.SetString(DataLibrary[1], jsonAudioData);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(DataLibrary[0]))
            {
                string jsonSaveData = PlayerPrefs.GetString(DataLibrary[0]);
                JsonUtility.FromJsonOverwrite(jsonSaveData, SaveData);
                File.WriteAllText(Application.dataPath + "/" + DataLibrary[0] + ".json", jsonSaveData);
            }
            else
            {
                SaveData = new SaveData();
                SaveData.SavedHighScore = 0;
                // Save();
            }

            if (PlayerPrefs.HasKey(DataLibrary[1]))
            {
                string jsonAudioData = PlayerPrefs.GetString(DataLibrary[1]);
                JsonUtility.FromJsonOverwrite(jsonAudioData, AudioData);
                File.WriteAllText(Application.dataPath + "/" + DataLibrary[1] + ".json", jsonAudioData);
            }
        }

        public void SetAudioData(AudioData data)
        {
            AudioData = data;
            SaveAudio();
        }

        public void SaveAudio()
        {
            // string jsonSaveData = JsonUtility.ToJson(SaveData);
            string jsonAudioData = JsonUtility.ToJson(AudioData);
            // File.WriteAllText(Application.dataPath + "/SavedData.json", jsonSaveData);
            File.WriteAllText(Application.dataPath + "/SavedAudioData.json", jsonAudioData);
            // PlayerPrefs.SetString(DataLibrary[0], jsonSaveData);
            PlayerPrefs.SetString(DataLibrary[1], jsonAudioData);
            PlayerPrefs.Save();
        }
    }
}