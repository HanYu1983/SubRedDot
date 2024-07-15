using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource backgroundMusic;
    public AudioSource sfxSource;

    void Start()
    {
        // 設置初始值和添加監聽器
        if (musicSlider != null)
        {
            musicSlider.value = backgroundMusic.volume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    void SetMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}