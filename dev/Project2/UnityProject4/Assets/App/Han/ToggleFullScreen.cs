using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class ToggleFullScreen : MonoBehaviour, IBaseButtonAction
    {
        public IEnumerator Perform()
        {
            Screen.fullScreen = !Screen.fullScreen;
            yield return null;
        }
    }
}