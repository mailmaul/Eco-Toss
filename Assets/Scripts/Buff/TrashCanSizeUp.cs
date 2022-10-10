using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Trash
{
public class TrashCanSizeUp : PoolObject
{
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject tongsampah;

       public static int score = 0;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("TrashOrganic"))
            {
                Vector3 objectScale = tongsampah.gameObject.transform.localScale;
                tongsampah.gameObject.transform.localScale = new Vector3(objectScale.x*1.2f,  objectScale.y*1.2f, objectScale.z*1.2f);
            }
        }

        public override void OnCreate()
        {

        }
    }
}
