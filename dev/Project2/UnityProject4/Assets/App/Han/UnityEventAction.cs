using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.App.Han
{
    public class UnityEventAction : AbstractBaseButtonAction
    {
        public UnityEvent OnClick;

        public override IEnumerator Perform()
        {
            OnClick.Invoke();
            yield return null;
        }
    }
}