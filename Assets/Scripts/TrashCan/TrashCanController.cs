using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Trash
{
    public class TrashCanController : PoolObject
    {
       [SerializeField] private Rigidbody _rigidbody;

       public static int score = 0;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("TrashOrganic"))
            {
                score++;
                Debug.Log("Skor anda" + score);
            }
        }

        public override void OnCreate()
        {

        }
    }
}