using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace EcoTeam.EcoToss.UI
{
    public class CutOutMaskUI : Image
    {
        public override Material materialForRendering
        {
            get
            {
                Material material = new Material(base.materialForRendering);
                material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
                return material;
            }
        }
    }
}