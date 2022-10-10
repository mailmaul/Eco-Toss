using Agate.MVC.Core;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Trash
{
    public class TrashController : PoolObject
    {
        
        public static int jumlahsampah = 0;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void StoreToPool()
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            base.StoreToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            
            if (collision.gameObject.CompareTag("TrashCanOrganic") && jumlahsampah <= 19)
            {
                PublishSubscribe.Instance.Publish<MessageTrashSpawn>(new MessageTrashSpawn());
                jumlahsampah++;
                Debug.Log("Jumlah Sampah" + jumlahsampah);


                // Store game object to pool and set active false
                Invoke("StoreToPool", 1);
                
            }
        }

        public override void OnCreate()
        {

        }
    }
}