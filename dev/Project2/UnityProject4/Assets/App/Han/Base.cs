using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class Base : MonoBehaviour
    {
        protected IEnumerator PerformMiddleware()
        {
            yield return null;
        }

        protected IEnumerator PerformAction()
        {
            //Debug.Log("PerformAction");
            yield return PerformMiddleware();
            var actions = GetComponents<IBaseButtonAction>();
            for (var i = 0; i < actions.Length; ++i)
            {
                var action = actions[i];
                //Debug.Log(action.GetType());
                yield return action.Perform();
            }
        }
    }
}