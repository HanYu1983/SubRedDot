using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class SetAudioSourceVolumeAction : AbstractBaseButtonAction
    {
        public AudioSource target;
        public override IEnumerator PerformFloat(float value)
        {
           /* var slider = GetComponent<Slider>();
            if(slider == null)
            {
                Debug.LogWarning("slider not found");
                yield return null;
            }*/
            if (target != null)
            {
                target.volume = value;
                yield break;
            }
            var audioSourceList = Resources.FindObjectsOfTypeAll<AudioSource>();
            for(var i =0; i< audioSourceList.Length; ++i)
            {
                var audioSource = audioSourceList[i];
                audioSource.volume = value;
            }
            yield return null;
        }
    }
}