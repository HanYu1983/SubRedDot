using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class SetSoundEffectVolumeAction : MonoBehaviour, IBaseButtonAction
    {
        public IEnumerator Perform()
        {
            var slider = GetComponent<Slider>();
            if(slider == null)
            {
                Debug.LogWarning("slider not found");
                yield return null;
            }
            var target = FindObjectOfType<GameState>().soundEffectAudioSource;
            if(target == null)
            {
                Debug.Log("musicAudioSource not found");
            }
            target.volume = slider.value;
        }
    }
}