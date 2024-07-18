using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class SetActiveLanguageFromGameState : MonoBehaviour, IBaseButtonAction
    {
        public IEnumerator Perform()
        {
            var localization = GetComponent<Localization>();
            if (localization == null)
            {
                Debug.LogWarning("localization not found");
                yield return null;
            }
            var gameState = FindObjectOfType<GameState>();
            if (gameState == null)
            {
                Debug.LogWarning("gameState not found");
                yield return null;
            }
            localization.SetActiveLanguage(gameState.languageCode, true);
        }
    }
}