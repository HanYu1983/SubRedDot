using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class DontDestroyOnLoadHan : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}