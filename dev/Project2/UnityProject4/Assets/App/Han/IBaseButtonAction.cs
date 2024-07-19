using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Han
{
    public interface IBaseButtonAction
    {
        IEnumerator Perform();
        IEnumerator PerformInt(int v);
        IEnumerator PerformFloat(float v);
        IEnumerator PerformString(string v);
    }
}