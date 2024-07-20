using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class OnFungusInvokeMethod : Base
    {

        public void OnFungusInvokeMethodCall()
        {
            StartCoroutine(PerformAction());
        }
    }
}