using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using EcoTeam.EcoToss.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public static int PlayerScore = 0;
    public static int Organic = 0;
    public static int NonOrganic = 0;
    public static int Dangerous = 0;

    public GUISkin layout;

    GameObject TrashOrganic;
    GameObject TrashNonOrganic;
    GameObject TrashDangerous;

    public bool IsWindSpawn { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        TrashOrganic    =   GameObject.FindGameObjectWithTag("TrashOrganic");
        TrashNonOrganic =   GameObject.FindGameObjectWithTag("TrashNonOrganic");
        TrashDangerous  =   GameObject.FindGameObjectWithTag("TrashDangerous");

        IsWindSpawn = false;
    }

    public static void Score (string trashCanID) {
        if (trashCanID == "TrashCanOrganic" || trashCanID == "TrashNonOrganic" || trashCanID == "TrashDangerous")
        {
            PlayerScore++;
            Debug.Log(PlayerScore);
            //PublishSubscribe.Instance.Unsubscribe<MessageTrashSpawn>(MessageTrashSpawnReceived);
            
        } 
        else
        {
            Organic++;
        }
    }

    public void OnWindSpawn(bool msg)
    {
        IsWindSpawn = msg;
    }

    void OnGUI () {
    GUI.skin = layout;
    GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore);
    GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + Organic);

    if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
    {
        PlayerScore = 0;
        Organic = 0;
    }

    if (PlayerScore == 10)
    {
        GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
    } else if (Organic == 10)
    {
        GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS");
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
