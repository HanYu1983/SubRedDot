using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class SetSliderValueBySoundEffectAudioSource : OnCustomAction
    {
        public override IEnumerator Perform()
        {
            var slider = GetComponent<Slider>();
            if(slider == null)
            {
                Debug.LogWarning("slider not found");
                yield break;
            }
            var gameState = FindObjectOfType<GameState>();
            if (gameState == null)
            {
                Debug.LogWarning("gameState not found");
                yield break;
            }
            var soundEffectAudioSource = gameState.soundEffectAudioSource;
            if (soundEffectAudioSource == null)
            {
                Debug.LogWarning("soundEffectAudioSource not found");
                yield break;
            }
            slider.value = soundEffectAudioSource.volume;
            yield return null;
        }
    }
}