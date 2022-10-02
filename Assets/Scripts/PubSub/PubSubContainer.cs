using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kelompok6.EcoToss.PubSub
{
    public class PubSubContainer { }

    public struct ExampleMessage
    {
        public string IDMessage;
        public int BodyMessage;

        public ExampleMessage(string iDMessage, int bodyMessage)
        {
            IDMessage = iDMessage;
            BodyMessage = bodyMessage;
        }
    }
}