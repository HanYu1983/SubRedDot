using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class OnAwakePerform : Base
    {
        private void Awake()
        {
            StartCoroutine(PerformAction());
        }
    }
}