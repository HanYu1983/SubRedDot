using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class ToggleFullScreen : AbstractBaseButtonAction
    {
        public override IEnumerator Perform()
        {
            Screen.fullScreen = !Screen.fullScreen;
            yield return null;
        }
    }
}