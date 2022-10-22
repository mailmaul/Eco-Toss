using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcoTeam.EcoToss.ObjectPooling;
using EcoTeam.EcoToss.PubSub;
using Agate.MVC.Core;

namespace EcoTeam.EcoToss.VFX
{
    public class VfxSpawnerController : MonoBehaviour
    {
        [SerializeField] private List<VfxObject> _vfxList = new List<VfxObject>();

        private PoolingSystem _pool = new PoolingSystem(10);

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnVFX>(Spawn);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnVFX>(Spawn);
        }

        //publish di script yang butuh vfx 
        public void Spawn(MessageSpawnVFX msg)
        {
            for(int i = 0; i < _vfxList.Count; i++)
            {
                if(_vfxList[i].name == msg.Name)
                {
                    _pool.CreateObject(_vfxList[i], msg.Position, transform);
                    return;
                }
            }
        }
    }
}

