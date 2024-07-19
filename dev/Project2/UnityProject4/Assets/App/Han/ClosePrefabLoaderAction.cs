using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class ClosePrefabLoaderAction : AbstractBaseButtonAction
    {
        public override IEnumerator Perform()
        {
            var targets = Resources.FindObjectsOfTypeAll<PrefabLoader>();
            for(var i=0; i<targets.Length; ++i)
            {
                var target = targets[i];
                yield return target.DestroyPrefab();
            }

           /* var target = FindObjectOfType<PrefabLoader>();
            if (target == null)
            {
                Debug.Log("PrefabLoader not found");
                yield return null;
            }
            yield return target.DestroyPrefab();*/
        }
    }
}