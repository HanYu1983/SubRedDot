using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class OnStartPerform : Base
    {
        private void Start()
        {
            StartCoroutine(PerformAction());
        }
    }
}