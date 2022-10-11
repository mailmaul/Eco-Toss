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

    public struct MessageSpawnIntruder
    {
        public int Index { get; private set; }

        public MessageSpawnIntruder(int index)
        {
            Index = index;
        }
    }
    
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

    public struct MessageSetRandomPropetiesWindArea { }

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
}