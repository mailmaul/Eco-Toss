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

    public struct MessageTrashSpawn
    {

    }
}