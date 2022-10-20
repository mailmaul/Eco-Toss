using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.PubSub
{
    public class PubSubContainer { }

    public struct MessageExample
    {
        public string IDMessage;
        public int BodyMessage;

        public MessageExample(string iDMessage, int bodyMessage)
        {
            IDMessage = iDMessage;
            BodyMessage = bodyMessage;
        }
    }

    public struct MessageTrashSpawn { }

    public struct MessageOnHitIntruder { }

    public struct MessageSpawnIntruder { }
    
    public struct MessageTrashThrowing
    {
        public Rigidbody TrashRigidbody;
        public Vector2 SwipeDirection;

        public MessageTrashThrowing(Rigidbody trashRigidbody, Vector2 swipeDirection)
        {
            TrashRigidbody = trashRigidbody;
            SwipeDirection = swipeDirection;
        }
    }
    
    public struct MessageAddScore
    {
        public string Amount;

        public MessageAddScore(string amount)
        {
            Amount = amount;
        }
    }

    public struct MessageRemoveScore
    {
        public string Amount;

        public MessageRemoveScore(string amount)
        {
            Amount = amount;
        }
    }

    public struct MessageStoreToPool { }

    public struct MessageSetRandomPropertiesWindArea { }

    public struct MessageShowWindProperties
    {
        public float Strength { get; private set; }
        public string Direction { get; private set; }

        public MessageShowWindProperties(float str, string dir)
        {
            Strength = str;
            Direction = dir;
        }
    }

    public struct MessageDecreaseHealth { }
  
    public struct MessageIncreaseHealth
    {
        public int AdditionalHealth { get; private set; }
        public MessageIncreaseHealth(int health)
        {
            AdditionalHealth = health;
        }
    }

    public struct MessageGameOver
    {
        public bool IsGameOver { get; private set; }
        public MessageGameOver(bool gameover)
        {
            IsGameOver = gameover;
        }
    }

    public struct MessageSpawnWindArea { }

    public struct MessageClearTrashList { }

    public struct MessagePlayBuff { }

    public struct MessageActivateDoubleScore { }

    public struct MessageDeactivateDoubleScore { }

    public struct MessageIncreaseTrashCanCapacity { }
    public struct MessageCheckPointSpawn { }
    public struct MessageCheckPointDestroy
    {
        public GameObject CheckPointObject { get; private set; }
        public MessageCheckPointDestroy(GameObject obj)
        {
            CheckPointObject = obj;
        }
    }

    public struct MessageSpawnVFX
    {
        public string Name { get; private set; }
        public Vector3 Position { get; private set; }
        public MessageSpawnVFX(string name, Vector3 pos)
        {
            Name = name;
            Position = pos;
        }
    }

    public struct MessagePlayBGM
    {
        public string Name { get; private set; }

        public MessagePlayBGM(string name)
        {
            Name = name;
        }
    }

    public struct MessagePlaySFX
    {
        public string Name { get; private set; }

        public MessagePlaySFX(string name)
        {
            Name = name;
        }
    }
}