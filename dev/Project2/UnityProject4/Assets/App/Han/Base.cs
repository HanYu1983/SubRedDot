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
                var baseAction = action as OnCustomAction;
                if (baseAction != null && baseAction.isCustomCall())
                {
                    continue;
                }
                yield return action.Perform();
            }
        }

        protected IEnumerator PerformFloatAction(float v)
        {
            //Debug.Log("PerformAction");
            yield return PerformMiddleware();
            var actions = GetComponents<IBaseButtonAction>();
            for (var i = 0; i < actions.Length; ++i)
            {
                var action = actions[i];
                var baseAction = action as OnCustomAction;
                if (baseAction != null && baseAction.isCustomCall())
                {
                    continue;
                }
                yield return action.PerformFloat(v);
            }
        }

        protected IEnumerator PerformIntAction(int v)
        {
            yield return PerformMiddleware();
            var actions = GetComponents<IBaseButtonAction>();
            for (var i = 0; i < actions.Length; ++i)
            {
                var action = actions[i];
                var baseAction = action as OnCustomAction;
                if (baseAction != null && baseAction.isCustomCall())
                {
                    continue;
                }
                yield return action.PerformInt(v);
            }
        }

        protected IEnumerator PerformStringAction(string v)
        {
            yield return PerformMiddleware();
            var actions = GetComponents<IBaseButtonAction>();
            for (var i = 0; i < actions.Length; ++i)
            {
                var action = actions[i];
                var baseAction = action as OnCustomAction;
                if (baseAction != null && baseAction.isCustomCall())
                {
                    continue;
                }
                yield return action.PerformString(v);
            }
        }
    }
}