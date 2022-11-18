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
            PlayerPrefs.SetString("SaveData", jsonSaveData);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("SaveData"))
            {
                string jsonSaveData = PlayerPrefs.GetString("SaveData");
                JsonUtility.FromJsonOverwrite(jsonSaveData, SaveData);
            }
            else
            {
                SaveData = new()
                {
                    SavedHighScore = 0
                };
                Save();
            }
        }
    }
}