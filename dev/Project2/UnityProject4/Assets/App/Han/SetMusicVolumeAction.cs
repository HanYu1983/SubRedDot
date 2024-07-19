using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class SetMusicVolumeAction : AbstractBaseButtonAction
    {
        public override IEnumerator PerformFloat(float value)
        {
            /*var slider = GetComponent<Slider>();
            if(slider == null)
            {
                Debug.LogWarning("slider not found");
                yield break;
            }*/
            var gameState = FindObjectOfType<GameState>();
            if(gameState == null)
            {
                Debug.LogWarning("musicAudioSource not found");
                yield break;
            }
            var musicAudioSource = gameState.musicAudioSource;
            if(musicAudioSource == null)
            {
                Debug.LogWarning("musicAudioSource not found");
                yield break;
            }
            musicAudioSource.volume = value;
        }
    }
}