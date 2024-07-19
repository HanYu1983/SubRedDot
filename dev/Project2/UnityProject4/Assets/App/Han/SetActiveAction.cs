using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class SetActiveAction : AbstractBaseButtonAction
    {
        public bool isActive;

        public override IEnumerator Perform()
        {
            gameObject.SetActive(isActive);
            yield return null;
        }
    }
}