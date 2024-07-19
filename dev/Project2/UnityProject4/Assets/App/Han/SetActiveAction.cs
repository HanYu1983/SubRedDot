using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class SetActiveAction : MonoBehaviour, IBaseButtonAction
    {
        public bool isActive;

        public IEnumerator Perform()
        {
            gameObject.SetActive(isActive);
            yield return null;
        }
    }
}