using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.Audio;

namespace EcoTeam.EcoToss.SaveData
{
    public class SaveAudioDataController : MonoBehaviour
    {
        public static SaveAudioDataController Instance;

        private const string key = "SaveAudioData";

        public AudioData AudioData;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            Load();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(AudioData);
            PlayerPrefs.SetString(key, json);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(json, AudioData);
            }
            else
            {
                AudioData = Resources.Load<AudioData>("Data/AudioData");
                Save();
            }
        }

        public void SetData(AudioData data)
        {
            AudioData = data;
            Save();
        }
    }
}

