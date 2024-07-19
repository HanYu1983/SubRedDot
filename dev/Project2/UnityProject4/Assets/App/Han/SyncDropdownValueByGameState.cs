using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class SyncDropdownValueByGameState : AbstractBaseButtonAction
    {

        void Start()
        {
            var dropdown = GetComponent<Dropdown>();
            if(dropdown == null)
            {
                Debug.LogWarning("dropdown not found");
                return;
            }
            var gameState = GetGameState();
            switch (gameState.GetLanguage())
            {
                case Language.Chinese:
                    dropdown.value = 0;
                    break;
                case Language.EN:
                    dropdown.value = 1;
                    break;
            }
        }

        public override IEnumerator PerformInt(int v)
        {
            var gameState = GetGameState();
            switch (v)
            {
                case 0:
                    gameState.SetLanguage(Language.Chinese);
                    break;
                case 1:
                    gameState.SetLanguage(Language.EN);
                    break;
                default:
                    throw new UnityException("unknown value");
            }
            yield return null;
        }
    }
}