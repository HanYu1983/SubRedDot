using System.Collections;
using UnityEngine;
using Fungus;

namespace Assets.App.Han
{
    public enum Language
    {
        Chinese, EN
    }
    public class GameState : MonoBehaviour
    {
        public string languageCode;
        public FungusManager fungusManager;
        public AudioSource soundEffectAudioSource;
        public AudioSource ambianceAudioSource;
        public AudioSource musicAudioSource;
        
        private void Awake()
        {
            AssignFungusInstance();
        }

        private void AssignFungusInstance()
        {
            fungusManager = FungusManager.Instance;
            musicAudioSource = fungusManager.GetComponents<AudioSource>()[0];
            ambianceAudioSource = fungusManager.GetComponents<AudioSource>()[1];
            soundEffectAudioSource = fungusManager.GetComponents<AudioSource>()[2];
        }

        public void SetLanguage(Language code)
        {
            switch (code)
            {
                case Language.Chinese:
                    languageCode = "";
                    break;
                case Language.EN:
                    languageCode = "EN";
                    break;
                default:
                    throw new UnityException("unknown Language");
            }
        }
        public Language GetLanguage()
        {
            switch (languageCode)
            {
                case "":
                    return Language.Chinese;
                case "EN":
                    return Language.EN;
                default:
                    throw new UnityException("unknown LanguageCode");
            }
        }
    }
}