using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public enum Language
    {
        Chinese, EN
    }
    public class GameState : MonoBehaviour
    {
        public string languageCode;
        public AudioSource soundEffectAudioSource;
        public AudioSource musicAudioSource;
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