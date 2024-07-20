using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class OnUpdatePerform : Base
    {
        public bool isOnce;
        private bool isOnceDone;
        private void Update()
        {
            if (isOnce)
            {
                if(isOnceDone == true)
                {
                    return;
                }
                StartCoroutine(PerformAction());
                isOnceDone = true;
                return;
            }
            StartCoroutine(PerformAction());
        }
    }
}