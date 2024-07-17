using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.App.Han
{
    public class UnityEventAction : MonoBehaviour, IBaseButtonAction
    {
        public UnityEvent OnClick;

        public IEnumerator Perform()
        {
            OnClick.Invoke();
            yield return null;
        }
    }
}