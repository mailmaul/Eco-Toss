using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoTeam.EcoToss.Animation
{
    public class Animation : MonoBehaviour
{

    private Animator _animator;

    private void FindGameObjects()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void NullChecking()
    {
        if(_animator == null)
        {
            Debug.LogError("haha");
        }
        else
        {
            Debug.LogError("ueay");
        }
    }
}

}
