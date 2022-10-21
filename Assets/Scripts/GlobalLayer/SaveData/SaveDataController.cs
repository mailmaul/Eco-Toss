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
            File.WriteAllText(Application.dataPath + "/SaveData.json", jsonSaveData);
            PlayerPrefs.SetString("SaveData", jsonSaveData);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("SaveData"))
            {
                string jsonSaveData = PlayerPrefs.GetString("SaveData");
                JsonUtility.FromJsonOverwrite(jsonSaveData, SaveData);
                File.WriteAllText(Application.dataPath + "/SaveData.json", jsonSaveData);
            }
            else
            {
                SaveData = new SaveData();
                SaveData.SavedHighScore = 0;
                Save();
            }
        }
    }
}