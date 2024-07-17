using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Han
{
    public interface IBaseButtonAction
    {
        IEnumerator Perform();
    }
}