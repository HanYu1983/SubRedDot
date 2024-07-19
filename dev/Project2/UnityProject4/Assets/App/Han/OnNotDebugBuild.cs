using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class OnNotDebugBuild : Base
    {
        void Awake()
        {
            if (Debug.isDebugBuild != true)
            {
                StartCoroutine(PerformAction());
            }
        }
    }
}