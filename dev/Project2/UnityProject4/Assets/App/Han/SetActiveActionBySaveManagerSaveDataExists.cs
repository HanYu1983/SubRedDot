using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class SetActiveActionBySaveManagerSaveDataExists : OnCustomAction
    {
        [SerializeField] 
        protected string saveDataKey = FungusConstants.DefaultSaveDataKey;

        public override IEnumerator Perform()
        {
            var saveManager = FungusManager.Instance.SaveManager;
            var isDataExists = saveManager.SaveDataExists(saveDataKey);
            Debug.Log(isDataExists + ":" + saveDataKey);
            var isActive = isDataExists;
            gameObject.SetActive(isActive);
            yield return null;
        }
    }
}