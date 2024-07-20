using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class SetActiveLanguageFromGameState : AbstractBaseButtonAction
    {
        public override IEnumerator Perform()
        {
            Debug.Log("XXX");
            var localization = GetComponent<Localization>();
            if (localization == null)
            {
                Debug.LogWarning("localization not found");
                yield break;
            }
            var gameState = FindObjectOfType<GameState>();
            if (gameState == null)
            {
                Debug.LogWarning("gameState not found");
                yield break;
            }
            Debug.Log("XXX2");
            localization.SetActiveLanguage(gameState.languageCode, true);
        }
    }
}