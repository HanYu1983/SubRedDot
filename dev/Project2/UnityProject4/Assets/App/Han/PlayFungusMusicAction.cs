using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public class PlayFungusMusicAction : OnCustomAction
    {
        public AudioClip audioClip;
        public bool isLoop = true;
        public float fadeDuration;
        public float atTime;
        public override IEnumerator Perform()
        {
            FungusManager.Instance.MusicManager.PlayMusic(audioClip, isLoop, fadeDuration, atTime);
            yield return null;
        }
    }
}