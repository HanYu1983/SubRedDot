using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

namespace Assets.App.Han
{
    public class SetMusicVolumeAction : AbstractBaseButtonAction
    {
        public override IEnumerator PerformFloat(float value)
        {
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