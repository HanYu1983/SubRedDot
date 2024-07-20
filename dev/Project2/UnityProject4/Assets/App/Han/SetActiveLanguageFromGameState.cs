using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class SetActiveLanguageFromGameState : AbstractBaseButtonAction
    {
        private void FixedUpdate()
        {
            StartCoroutine(Perform());
        }
        public override IEnumerator Perform()
        {
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
            localization.SetActiveLanguage(gameState.languageCode, true);
        }
    }
}